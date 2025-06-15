using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class StickManEnemy : MonoBehaviour
    {
        [field: SerializeField] public float AttackRange { get; private set; } = 6f;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rb;
        [Space]
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _rotationSpeed;

        [Inject] private EnemyStateFactory _enemyStateFactory;

        private IEnemyState _currentState;

        private void OnEnable ()
        {
            ChangeState<PatrolState>();
        }

        public void ChangeState<T>() where T : IEnemyState
        {
            _currentState = _enemyStateFactory.Create<T>();
            _currentState.EnterState(this);
        }

        public void SetAnimation(string trigger) => _animator.SetTrigger(trigger);

        public void MoveToTarget(Vector3 targetPosition, bool isRun)
        {
            var moveSpeed = isRun ? _runSpeed : _walkSpeed;
            var direction = (targetPosition - transform.position).normalized;

            _rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);

            var toRotation = Quaternion.LookRotation(direction, Vector3.up);
            _rb.MoveRotation(Quaternion.Slerp(transform.rotation, toRotation, _rotationSpeed * Time.fixedDeltaTime));
        }

        private void Update()
        {
            _currentState.UpdateState(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }
    }
}