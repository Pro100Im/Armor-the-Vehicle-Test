using Zenject;

namespace Game.Enemy
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PatrolState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChaseState>().AsSingle();
            Container.BindInterfacesAndSelfTo<AttackState>().AsSingle();
        }
    }
}