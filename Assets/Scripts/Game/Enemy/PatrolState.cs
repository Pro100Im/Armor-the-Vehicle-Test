using Game.Car;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class PatrolState : IEnemyState
    {
        [Inject] private ICarPosition _carPosition;

        private string _animTrigger = "Idle";
        private string _animAlterTrigger = "Walk";

        private Vector3 _spawnPosition;
        private Vector3 _targetPosition;

        private float _patrolRadius;

        public void EnterState(StickManEnemy enemy)
        {
            enemy.SetAnimation(_animTrigger);

            _spawnPosition = enemy.transform.position;
            _patrolRadius = enemy.AttackRange;
        }

        public void UpdateState(StickManEnemy enemy)
        {
            var distanceToPlayer = Vector3.Distance(enemy.transform.position, _carPosition.Get());

            if(distanceToPlayer <= _patrolRadius)
            {
                enemy.ChangeState<AttackState>();
            }
            else
            {
                enemy.MoveToTarget(_targetPosition, false);
                enemy.SetAnimation(_animAlterTrigger);

                if(Vector3.Distance(enemy.transform.position, _targetPosition) < 0.5f)
                    SetNewPatrolPoint();
            }
        }

        private void SetNewPatrolPoint()
        {
            var randomOffset = Random.insideUnitCircle * _patrolRadius;
            _targetPosition = _spawnPosition + new Vector3(randomOffset.x, 0, randomOffset.y);
        }
    }
}