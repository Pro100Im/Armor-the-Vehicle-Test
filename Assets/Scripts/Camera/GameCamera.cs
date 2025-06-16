using Unity.Cinemachine;
using UnityEngine;

namespace CustomCamera
{
    public class GameCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCameraBase _menuCamera;
        [SerializeField] private CinemachineVirtualCameraBase _gameCamera;

        public void ActiveGameCamera()
        {
            _menuCamera.Priority = (int)CameraPriority.Off;
            _gameCamera.Priority = (int)CameraPriority.On;
        }

        public void ActiveMenuCamera()
        {
            _menuCamera.Priority = (int)CameraPriority.On;
            _gameCamera.Priority = (int)CameraPriority.Off;
        }
    }
}