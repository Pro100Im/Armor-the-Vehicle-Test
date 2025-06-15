using Game.Car;
using UnityEngine;
using Zenject;

namespace Game.Road
{
    public class LevelRoad : MonoBehaviour
    {
        [SerializeField] private float _roadLength = 105f;
        [SerializeField] private float _offset = 10f;
        [Space]
        [SerializeField] private PartRoad[] _roads;

        [Inject] private ICarPosition _carPosition;

        private int firstRoadIndex = 0;

        public void Init()
        {
            foreach (var road in _roads)
                road.FirstInit();
        }

        private void Update()
        {
            var playerZ = _carPosition.GetZ();
            var firstRoadZ = _roads[firstRoadIndex].transform.position.z;

            if(playerZ > firstRoadZ + _roadLength / 2 + _offset)
            {
                var lastRoadIndex = (firstRoadIndex + _roads.Length - 1) % _roads.Length;
                var newPos = _roads[lastRoadIndex].transform.position + Vector3.forward * _roadLength;

                _roads[firstRoadIndex].Setup(newPos);

                firstRoadIndex = (firstRoadIndex + 1) % _roads.Length;
            }
        }
    }
}