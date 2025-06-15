using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class StickManEnemy : MonoBehaviour
    {
        [field: SerializeField] public float AttackRange { get; private set; } = 6f;

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