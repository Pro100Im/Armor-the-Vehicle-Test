using Game.Car;
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
        [Space]
        [SerializeField] private float _walkSpeed = 0.3f;
        [SerializeField] private float _runSpeed = 6f;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private float _attackDamage = 3f;
        [Space]
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private LayerMask _bulletLayer;

        [Inject] private EnemyStateFactory _enemyStateFactory;

        private IEnemyState _currentState;

        private void Awake()
        {
            _dieEffect.transform.parent = null;
        }

        public void Init(Vector3 spawnPosition)
        {
            SpawnPosition = spawnPosition;
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

        public void Die()
        {
            _dieEffect.transform.position = transform.position;
            _dieEffect.Play();
        }

        private void Update()
        {
            _currentState.UpdateState(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(((1 << other.gameObject.layer) & _playerLayer) != 0)
            {
                ChangeState<AttackState>();

                other.GetComponent<CarHp>().TakeDamage(_attackDamage);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }
    }
}