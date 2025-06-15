namespace Game.Enemy
{
    public interface IEnemyState
    {
        void EnterState(StickManEnemy enemy);
        void UpdateState(StickManEnemy enemy);
    }
}