using Zenject;

namespace Game
{
    public class GameUiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<StartTutorial>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ProgressBar>().FromComponentInHierarchy().AsSingle();
        }
    }
}