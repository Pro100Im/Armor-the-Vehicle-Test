using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private StickManEnemy _stickManPrefab;
        [Space]
        [SerializeField] private int _poolSize = 150;
        [Space]
        [SerializeField] private float _maxRandomSpawnRot = 360;

        [Inject] private readonly DiContainer _container;

        private Queue<StickManEnemy> _enemyPool = new Queue<StickManEnemy>();
        private List<StickManEnemy> _activeEnemies = new List<StickManEnemy>();

        public List<StickManEnemy> ActiveEnemies => _activeEnemies;


        private void Awake()
        {
            for(int i = 0; i < _poolSize; i++)
            {
                var enemy = _container.InstantiatePrefabForComponent<StickManEnemy>(_stickManPrefab, transform);
                enemy.gameObject.SetActive(false);

                _enemyPool.Enqueue(enemy);
            }
        }

        public StickManEnemy Spawn(Vector3 position, Transform parent)
        {
            StickManEnemy enemy;

            if(_enemyPool.Count > 0)
                enemy = _enemyPool.Dequeue();
            else
                enemy = Instantiate(_stickManPrefab);

            var randomY = Random.Range(0f, _maxRandomSpawnRot);
            var rotation = Quaternion.Euler(0f, randomY, 0f);

            enemy.transform.parent = null;
            enemy.Init(position);
            enemy.gameObject.SetActive(true);

            _activeEnemies.Add(enemy);

            return enemy;
        }

        public void Despawn(StickManEnemy enemy)
        {
            enemy.gameObject.SetActive(false);
            enemy.transform.parent = transform;

            if(!_activeEnemies.Contains(enemy))
                return;

            _activeEnemies.Remove(enemy);
            _enemyPool.Enqueue(enemy);
        }

        public void DespawnAll()
        {
            foreach(var enemy in _activeEnemies)
            {
                enemy.gameObject.SetActive(false);
                enemy.transform.parent = transform;

                _enemyPool.Enqueue(enemy);
            }

            _activeEnemies.Clear();
        }
    }
}