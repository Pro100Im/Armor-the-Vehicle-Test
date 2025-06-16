using UnityEngine;

namespace Game.Enemy
{
    public class EnemyHpBar : HpBar
    {
        private Camera _camera;
        private Quaternion _initialLocalRotation;

        private void Start()
        {
            _camera = Camera.main;
            _initialLocalRotation = transform.localRotation;
        }

        private void LateUpdate()
        {
            transform.localRotation = _initialLocalRotation;

            var directionToCamera = _camera.transform.position - transform.position;

            var lookRotation = Quaternion.LookRotation(directionToCamera);
            transform.rotation = lookRotation * _initialLocalRotation;
        }
    }
}