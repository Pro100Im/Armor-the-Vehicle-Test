using System.Collections;
using UnityEngine;

namespace Game.Bullet
{
    public class BaseBullet : MonoBehaviour
    {
        [field: SerializeField] public int Damage { get; private set; } = 30;
        [Space]
        [SerializeField] private float speedForce = 5f;
        [SerializeField] private float lifeTime = 3f;
        [Space]
        [SerializeField] private Rigidbody rb;

        private IBulletDeSpawner _bulletPool;
        private Coroutine _coroutine;

        public void Init(IBulletDeSpawner bulletPool)
        {
            _bulletPool = bulletPool;
        }

        public void AddForce()
        {
            rb.AddForce(transform.up * speedForce, ForceMode.Impulse);

            _coroutine = StartCoroutine(LifeTime());
        }

        private IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(lifeTime);

            _bulletPool.Despawn(this);
        }

        public void Despawn()
        {
            StopCoroutine(_coroutine);

            _bulletPool.Despawn(this);
        }

        private void OnDisable()
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}