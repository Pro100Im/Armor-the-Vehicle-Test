using Zenject;

namespace Camera
{
    public class CameraInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameCamera>().FromComponentInHierarchy().AsSingle();
        }
    }
}