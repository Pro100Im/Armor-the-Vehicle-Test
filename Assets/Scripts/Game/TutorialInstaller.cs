using Zenject;

namespace Game
{
    public class TutorialInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<StartTutorial>().FromComponentInHierarchy().AsSingle();
        }
    }
}