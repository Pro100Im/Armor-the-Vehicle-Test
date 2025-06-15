using Zenject;

namespace Game.Enemy
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyStateFactory>().AsSingle();
            Container.Bind<IEnemyState>().To<PatrolState>().AsTransient();
            Container.Bind<IEnemyState>().To<ChaseState>().AsTransient();
            Container.Bind<IEnemyState>().To<AttackState>().AsTransient();
        }
    }
}