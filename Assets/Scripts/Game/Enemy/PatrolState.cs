using Game.Car;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class PatrolState : IEnemyState
    {
        [Inject] private ICarPosition _carPosition;

        private const float _waitingTime = 4f;

        private string _animState = "Idle";
        private string _animAlterState = "Walking";

        private Vector3 _targetPosition;

        private float _waitTimer;

        private bool _isMoving;

        public void EnterState(StickManEnemy enemy)
        {
            _targetPosition = enemy.transform.position;
        }

        public void UpdateState(StickManEnemy enemy)
        {
            float distanceToPlayer = Vector3.Distance(enemy.transform.position, _carPosition.Get());

            if(distanceToPlayer <= enemy.AttackRange)
            {
                enemy.ChangeState<ChaseState>();

                return;
            }

            Patrol(enemy);
        }

        private void Patrol(StickManEnemy enemy)
        {
            if(Vector3.Distance(enemy.transform.position, _targetPosition) <= 0.5f && _waitTimer <= 0)
            {
                _isMoving = false;

                if(Random.value < 0.5f)
                {
                    _waitTimer = Random.Range(1f, _waitingTime);

                    enemy.SetAnimation(_animState);
                }
                else
                {
                    var offset = Random.insideUnitCircle * enemy.PatrolRange;
                    _targetPosition = enemy.SpawnPosition + new Vector3(offset.x, 0, offset.y);
                    _isMoving = true;

                    enemy.SetAnimation(_animAlterState);
                }          
            }
            else if(_isMoving)
            {
                enemy.MoveToTarget(_targetPosition, false);
            }
            else
                _waitTimer -= Time.deltaTime;
        }
    }
}