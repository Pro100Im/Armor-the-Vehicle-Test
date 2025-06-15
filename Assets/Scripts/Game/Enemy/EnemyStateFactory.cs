using Zenject;

namespace Game.Enemy
{
    public class EnemyStateFactory
    {
        [Inject] private readonly DiContainer _container;

        public T Create<T>() where T : IEnemyState
        {
            return _container.Instantiate<T>();
        }
    }
}
