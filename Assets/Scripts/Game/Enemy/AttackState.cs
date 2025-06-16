using Cysharp.Threading.Tasks;
using Game.Car;
using System;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class AttackState : IEnemyState
    {
        [Inject] ICarPosition _carPosition;

        private const string _animState = "Attack";

        public void EnterState(StickManEnemy enemy)
        {
            enemy.SetAnimation(_animState);
            enemy.Die().Forget();
        }

        public void UpdateState(StickManEnemy enemy)
        {
            if(Vector3.Distance(enemy.transform.position, _carPosition.Get()) > enemy.AttackRange)
                enemy.Die().Forget();
        }
    }
}