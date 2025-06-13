using Zenject;

namespace Game
{
    public class GameModeInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameMode>().FromComponentInHierarchy().AsSingle();
        }
    }
}