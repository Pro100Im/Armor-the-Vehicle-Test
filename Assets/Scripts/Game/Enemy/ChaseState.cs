using Game.Car;
using Zenject;

namespace Game.Enemy
{
    public class ChaseState : IEnemyState
    {
        [Inject] private ICarPosition _carPosition;

        private string _animState = "Runing";

        public void EnterState(StickManEnemy enemy)
        {
            enemy.SetAnimation(_animState);
        }

        public void UpdateState(StickManEnemy enemy)
        {
            Chase(enemy);
        }

        private void Chase(StickManEnemy enemy)
        {
            var targetPosition = _carPosition.Get();

            enemy.MoveToTarget(targetPosition, true);
        }
    }
}