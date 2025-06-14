using Assets.Scripts.Game.Car;
using Cysharp.Threading.Tasks;
using Game.Car;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameMode : MonoBehaviour
    {
        [Inject] private StartTutorial _startTutorial;
        [Inject] private CarMovement _carMovement;
        [Inject] private CarTurret _carTurret;

        public async UniTask PrepareToStart()
        {
            _startTutorial.Show();

            await _startTutorial.TutorialProcess();

            StartGame();
        }

        private void StartGame()
        {
            _carMovement.StartMoving();
            _carTurret.Init();
        }

        public void EndGame()
        {

        }
    }
}