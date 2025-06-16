using Zenject;

namespace Game.Enemy
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyPool>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemyStateFactory>().FromNew().AsSingle();
            Container.Bind<IEnemyState>().To<PatrolState>().AsTransient();
            Container.Bind<IEnemyState>().To<ChaseState>().AsTransient();
            Container.Bind<IEnemyState>().To<AttackState>().AsTransient();
        }
    }
}