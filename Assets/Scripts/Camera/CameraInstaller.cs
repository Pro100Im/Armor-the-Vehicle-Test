using Zenject;

namespace CustomCamera
{
    public class CameraInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameCamera>().FromComponentInHierarchy().AsSingle();
        }
    }
}