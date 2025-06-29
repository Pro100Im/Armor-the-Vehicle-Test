using Assets.Scripts.Game.Car;
using Zenject;

namespace Game.Car
{
    public class CarInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CarMovement>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CarTurret>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CarHp>().FromComponentInHierarchy().AsSingle();
        }
    }
}