using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class StickManEnemy : MonoBehaviour
    {
        [field: SerializeField] public float AttackRange { get; private set; } = 6f;

        [Inject] private PatrolState _patrolState;

        private IEnemyState _currentState;

        private void Start()
        {
            ChangeState(_patrolState);
        }

        public void ChangeState(IEnemyState newState)
        {
            _currentState = newState;
            _currentState.EnterState(this);
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