
namespace Game.Enemy
{
    public class WaitState : IEnemyState
    {
        private string _animState = "Idle";

        public void EnterState(StickManEnemy enemy)
        {
            enemy.SetAnimation(_animState);
        }

        public void UpdateState(StickManEnemy enemy)
        {
            
        }
    }
}