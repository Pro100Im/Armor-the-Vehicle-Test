using Game.Bullet;
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
        [SerializeField] private float _sensitivity = 0.5f;
        [SerializeField] private float _fireRate = 1f;
        [Space]
        [SerializeField] protected Transform _firePoint;
        [SerializeField] protected BulletPool _bulletPool;
        [SerializeField] protected ParticleSystem _fireEffect;
        //[SerializeField] protected Audio _fireAudio;

        private float _targetRotation;
        private float _currentRotation;
        private float _nextFireTime;

        private bool _inited;

        public void Init()
        {
            _aimAction.Enable();
            _aimAction.performed += SetTargetRotation;

            _turretLaser.SetActive(true);
            _inited = true;
        }

        private void SetTargetRotation(InputAction.CallbackContext context)
        {
            float input = context.ReadValue<float>() * _sensitivity;
            float deltaX = input * _rotationSpeed;

            _targetRotation = Mathf.Clamp(_targetRotation + deltaX, -_maxRotationAngle, _maxRotationAngle);
        }

        private void Update()
        {
            if(!_inited) return;

            if(Time.time >= _nextFireTime)
            {
                Shoot();
                _nextFireTime = Time.time + _fireRate;
            }

            _currentRotation = Mathf.Lerp(_currentRotation, _targetRotation, Time.deltaTime * _rotationSpeed);
            var currentEuler = transform.eulerAngles;
            currentEuler.y = _currentRotation;

            transform.eulerAngles = currentEuler;
        }

        private void Shoot()
        {
            _fireEffect.Play();

            var bullet = _bulletPool.Spawn(_firePoint.position, _firePoint.rotation);
            bullet.AddForce();
        }

        private void OnDestroy()
        {
            _aimAction.performed -= SetTargetRotation;
            _aimAction.Disable();
        }
    }
}