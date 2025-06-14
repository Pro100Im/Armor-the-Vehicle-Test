using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Car
{
    public class CarTurret : MonoBehaviour
    {
        [SerializeField] private GameObject _turretLaser;
        [SerializeField] private InputAction _aimAction;
        [Space]
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _maxRotationAngle = 60f;

        private float currentRotation = 0f;

        public void Init()
        {
            _aimAction.Enable();
            _aimAction.performed += RotateTurret;

            _turretLaser.SetActive(true);
        }

        private void RotateTurret(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<float>();
            var deltaX = input * _rotationSpeed * Time.deltaTime;
            var currentEuler = transform.eulerAngles;

            currentRotation = Mathf.Clamp(currentRotation + deltaX, -_maxRotationAngle, _maxRotationAngle);
            currentEuler.y = currentRotation;
            transform.eulerAngles = currentEuler;
        }

        private void OnDestroy()
        {
            _aimAction.performed -= RotateTurret;
            _aimAction.Disable();
        }
    }
}