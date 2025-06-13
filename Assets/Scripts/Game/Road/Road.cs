using Game.Car;
using UnityEngine;
using Zenject;

namespace Game.Road
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private float _roadLength = 105f;
        [SerializeField] private float _offset = 10f;
        [Space]
        [SerializeField] private Transform[] _roads;

        [Inject] private ICarPosition _carPosition;

        private int firstRoadIndex = 0;

        private void Update()
        {
            var playerZ = _carPosition.GetZ();
            var firstRoadZ = _roads[firstRoadIndex].position.z;

            if(playerZ > firstRoadZ + _roadLength / 2 + _offset)
            {
                int lastRoadIndex = (firstRoadIndex + _roads.Length - 1) % _roads.Length;

                _roads[firstRoadIndex].position = _roads[lastRoadIndex].position + Vector3.forward * _roadLength;

                firstRoadIndex = (firstRoadIndex + 1) % _roads.Length;
            }
        }
    }
}