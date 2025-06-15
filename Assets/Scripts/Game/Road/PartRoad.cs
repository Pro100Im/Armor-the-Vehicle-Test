using Game.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Road
{
    public class PartRoad : MonoBehaviour
    {
        [SerializeField] private EnemyPool _enemyPool;
        [Space]
        [SerializeField] private Vector3 _spawnAreaSize = new Vector3(10, 0, 10);
        [SerializeField] private float _minDistance = 1f;
        [SerializeField] private int _minEnemyCount = 10;
        [SerializeField] private int _maxEnemyCount = 50;

        private List<Vector3> _spawnPositions = new List<Vector3>();

        public void FirstInit() => Setup(transform.position);

        public void Setup(Vector3 newPos)
        {
            _enemyPool.DespawnAll();

            transform.position = newPos;

            SetupEnemies();
        }

        private void SetupEnemies()
        {
            _spawnPositions.Clear();

            var enemyCount = Random.Range(_minEnemyCount, _maxEnemyCount + 1);
            var attempts = 0;

            while(_spawnPositions.Count < enemyCount && attempts < enemyCount * 2)
            {
                var localPos = new Vector3(Random.Range(-_spawnAreaSize.x / 2, _spawnAreaSize.x / 2), 0, Random.Range(-_spawnAreaSize.z / 2, _spawnAreaSize.z / 2));
                var worldPos = transform.position + localPos;
                var valid = true;

                foreach(var pos in _spawnPositions)
                {
                    float dist = Vector3.Distance(worldPos, pos);

                    if(dist < _minDistance)
                    {
                        valid = false;
                        break;
                    }
                }

                if(valid || _spawnPositions.Count == 0)
                {
                    _spawnPositions.Add(worldPos);

                    var enemy = _enemyPool.Spawn(worldPos, transform);
                    enemy.transform.position = worldPos;
                    enemy.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log($"valid {valid} || _spawnPositions.Count {_spawnPositions.Count}");
                }

                attempts++;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, _spawnAreaSize);
        }
    }
}