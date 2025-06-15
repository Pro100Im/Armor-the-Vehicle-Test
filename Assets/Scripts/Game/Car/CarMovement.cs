using DG.Tweening;
using Game.Car;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game.Car
{
    public class CarMovement : MonoBehaviour, ICarPosition
    {
        [Header("Movement Settings")]
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _swayAmplitude = 5f;
        [SerializeField] private float _swayFrequency = 1f;
        [SerializeField] private float _turnSmoothTime = 0.3f;
        [SerializeField] private float _swayOffsetAmount = 0.5f;
        [Space]
        [SerializeField] private float _roadMinX = -2.5f;
        [SerializeField] private float _roadMaxX = 2.5f;
        [SerializeField] private float _roadSafeMargin = 0.3f;
        [Space]
        [SerializeField] private float _initialSpeedMultiplier = 2f;
        [SerializeField] private float _accelerationDistance = 10f;

        private bool _isMoving;
        private Coroutine _moveCoroutine;

        public float GetZ() => transform.position.z;

        public Vector3 Get() => transform.position;

        public void StartMoving()
        {
            if(_isMoving) return;
            _isMoving = true;

            _moveCoroutine = StartCoroutine(MoveRoutine());
        }

        public void StopMoving()
        {
            _isMoving = false;
            if(_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);
        }

        private IEnumerator MoveRoutine()
        {
            var time = 0f;
            var noiseSeed = Random.Range(0f, 100f);
            var traveledDistance = 0f;

            while(_isMoving)
            {
                // Определяем текущую скорость
                var currentSpeed = traveledDistance < _accelerationDistance ? _speed * _initialSpeedMultiplier : _speed;

                var noise = (Mathf.PerlinNoise(noiseSeed, time * _swayFrequency) - 0.5f) * 2f;
                var swayAngle = noise * _swayAmplitude;
                var swayOffset = noise * _swayOffsetAmount;

                var forward = transform.forward * currentSpeed * Time.deltaTime;
                var lateral = transform.right * swayOffset * Time.deltaTime;

                var nextPos = transform.position + forward + lateral;

                var clampedSwayOffset = swayOffset;
                var clampedSwayAngle = swayAngle;

                if(nextPos.x < _roadMinX + _roadSafeMargin && swayOffset < 0f)
                {
                    var factor = Mathf.InverseLerp(_roadMinX, _roadMinX + _roadSafeMargin, nextPos.x);
                    clampedSwayOffset *= factor;
                    clampedSwayAngle *= factor;
                    nextPos = transform.position + forward + transform.right * clampedSwayOffset * Time.deltaTime;
                }
                else if(nextPos.x > _roadMaxX - _roadSafeMargin && swayOffset > 0f)
                {
                    var factor = Mathf.InverseLerp(_roadMaxX, _roadMaxX - _roadSafeMargin, nextPos.x);
                    clampedSwayOffset *= factor;
                    clampedSwayAngle *= factor;
                    nextPos = transform.position + forward + transform.right * clampedSwayOffset * Time.deltaTime;
                }

                var targetRot = Quaternion.Euler(0f, clampedSwayAngle, 0f);
                transform.DORotateQuaternion(targetRot, _turnSmoothTime);

                transform.position = nextPos;

                traveledDistance += currentSpeed * Time.deltaTime;
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}