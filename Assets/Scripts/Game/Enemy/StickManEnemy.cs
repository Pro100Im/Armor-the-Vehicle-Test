using Cysharp.Threading.Tasks;
using Game.Bullet;
using Game.Car;
using System;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class StickManEnemy : MonoBehaviour
    {
        [field: SerializeField] public float AttackRange { get; private set; } = 6f;
        [field: SerializeField] public float PatrolRange { get; private set; } = 3f;
        [field: SerializeField] public Vector3 SpawnPosition { get; private set; }

        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private ParticleSystem _dieEffect;
        [SerializeField] private EnemyHpBar _hpBar;
        [Space]
        [SerializeField] private float _walkSpeed = 0.3f;
        [SerializeField] private float _runSpeed = 6f;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private float _attackDamage = 3f;
        [SerializeField] private float _maxHp = 100f;
        [SerializeField] private float _hitDelay = 0.3f;
        [Space]
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private LayerMask _bulletLayer;

        [Inject] private EnemyStateFactory _enemyStateFactory;
        [Inject] EnemyPool _pool;

        private float _currentHp;
        private IEnemyState _currentState;

        private void Awake()
        {
            _dieEffect.transform.parent = null;
        }

        public void Init(Vector3 spawnPosition)
        {
            _hpBar.gameObject.SetActive(false);
            _currentHp = _maxHp;
            _hpBar.ChangeHpView(_currentHp, _maxHp);

            SpawnPosition = spawnPosition;
            transform.position = spawnPosition;
            ChangeState<PatrolState>();
        }

        public void ChangeState<T>() where T : IEnemyState
        {
            _currentState = _enemyStateFactory.Create<T>();
            _currentState.EnterState(this);
        }

        public void SetAnimation(string stateName) => _animator.Play(stateName);

        public void MoveToTarget(Vector3 targetPosition, bool isRun)
        {
            var moveSpeed = isRun ? _runSpeed : _walkSpeed;
            var direction = (targetPosition - transform.position).normalized;

            _rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);

            var toRotation = Quaternion.LookRotation(direction, Vector3.up);
            _rb.MoveRotation(Quaternion.Slerp(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime));
        }

        public async UniTask Die()
        {
            _dieEffect.transform.position = transform.position;
            _dieEffect.Play();

            await UniTask.Delay(TimeSpan.FromSeconds(_hitDelay));

            _pool.Despawn(this);
        }

        private void Update()
        {
            _currentState.UpdateState(this);

            if(_currentHp < _maxHp && !_hpBar.isActiveAndEnabled)
                _hpBar.gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(((1 << other.gameObject.layer) & _playerLayer) != 0)
            {
                ChangeState<AttackState>();

                other.GetComponent<CarHp>().TakeDamage(_attackDamage);
            }
            else if(((1 << other.gameObject.layer) & _bulletLayer) != 0)
            {
                var bullet = other.GetComponent<BaseBullet>();

                _currentHp -= bullet.Damage;
                _currentHp = _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);

                bullet.Despawn();

                _hpBar.ChangeHpView(_currentHp, _maxHp);

                if(_currentHp <= 0)
                {
                    Die().Forget();
                }
            }
        }

        private async UniTaskVoid TakeDamge()
        {
            await Die();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }
    }
}