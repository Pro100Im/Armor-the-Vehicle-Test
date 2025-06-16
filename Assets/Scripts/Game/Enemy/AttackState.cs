using Cysharp.Threading.Tasks;
using System;
using Zenject;

namespace Game.Enemy
{
    public class AttackState : IEnemyState
    {
        [Inject] EnemyPool _pool;

        private const string _animState = "Attack";
        private const float _dieDelay = 0.3f;

        public void EnterState(StickManEnemy enemy) => EnterStateAsync(enemy).Forget();

        public async UniTaskVoid EnterStateAsync(StickManEnemy enemy)
        {
            enemy.SetAnimation(_animState);
            enemy.Die();

            await UniTask.Delay(TimeSpan.FromSeconds(_dieDelay));

            _pool.Despawn(enemy);
        }

        public void UpdateState(StickManEnemy enemy)
        {
            
        }
    }
}