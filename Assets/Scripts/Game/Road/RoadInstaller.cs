using Zenject;

namespace Game.Road
{
    public class RoadInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelRoad>().FromComponentInHierarchy().AsSingle();
        }
    }
}