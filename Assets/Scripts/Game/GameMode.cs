using Assets.Scripts.Game.Car;
using Cysharp.Threading.Tasks;
using Game.Car;
using Game.Enemy;
using Game.Road;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game
{
    public class GameMode : MonoBehaviour
    {
        [SerializeField] private float _levelDistance = 100;

        [Inject] private StartTutorial _startTutorial;
        [Inject] private ProgressBar _progressBar;
        [Inject] private CarMovement _carMovement;
        [Inject] private CarHp _carHp;
        [Inject] private CarTurret _carTurret;
        [Inject] private LevelRoad _road;
        [Inject] private EndLevel _endLevel;
        [Inject] private EnemyPool _enemyPool;

        private float _startPos;
        private float _levelGoal;
        private float _currentLevel;

        private bool _isPlay;

        private void Start()
        {
            _endLevel.NextButton.onClick.AddListener(NextLevel);
            _endLevel.RestartButton.onClick.AddListener(Restart);
        }

        public async UniTask PrepareToStart()
        {
            _currentLevel = 1;
            _road.Init();
            _startTutorial.Show();
            _progressBar.Show();
            _startPos = _carMovement.GetZ();
            _carHp.Show();

            await _startTutorial.TutorialProcess();

            StartLevel();
        }

        private void StartLevel()
        {
            _levelGoal = _levelDistance * _currentLevel;
            _carMovement.StartMoving();
            _carTurret.On();

            _isPlay = true;
        }

        private void Update()
        {
            if(!_isPlay)
                return;

            var progress = Mathf.Abs(_carMovement.GetZ() - _startPos);

            _progressBar.SetProgress(progress, _levelGoal);

            if(!_carHp.IsAlive())
            {
                LoseLevel();
            }
            else if(progress >= _levelGoal)
            {
                WinLevel();
            }
        }

        private void WinLevel()
        {
            _endLevel.WinLevel();
            EndLevel();
        }

        private void LoseLevel()
        {
            _endLevel.LoseLevel();
            EndLevel();
        }

        private void EndLevel()
        {
            _isPlay = false;

            foreach(var enemy in _enemyPool.ActiveEnemies)
                enemy.ChangeState<WaitState>();

            _carMovement.StopMoving();
            _carTurret.Off();
            _progressBar.Hide();
            _carHp.Hide();
        }

        private void NextLevel()
        {
            _currentLevel++;
            _startPos = _carMovement.GetZ();
            _progressBar.Show();
            _carHp.Show();

            StartLevel();

            foreach(var enemy in _enemyPool.ActiveEnemies)
                enemy.ChangeState<PatrolState>();
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}