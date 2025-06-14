using System.Collections;
using UnityEngine;

namespace Game.Bullet
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected int damage = 1;
        [Space]
        [SerializeField] private float speedForce = 5f;
        [SerializeField] private float lifeTime = 3f;
        [Space]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private LayerMask layer;

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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(((1 << collision.gameObject.layer) & layer.value) != 0)
                return;

            if(_coroutine != null)
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