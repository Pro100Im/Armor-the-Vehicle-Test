using Zenject;

namespace Game.Enemy
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyStateFactory>().AsSingle();
            Container.Bind<IEnemyState>().To<PatrolState>().AsSingle();
            Container.Bind<IEnemyState>().To<ChaseState>().AsSingle();
            Container.Bind<IEnemyState>().To<AttackState>().AsSingle();
        }
    }
}