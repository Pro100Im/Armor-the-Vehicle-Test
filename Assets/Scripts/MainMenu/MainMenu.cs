using Camera;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        [Space]
        [SerializeField] private float _fadeDuration = 0.5f;

        [Inject] private GameCamera _gameCamera;
        [Inject] private GameMode _gameMode;

        private float _hidePos;

        private void Awake()
        {
            _startButton.onClick.AddListener(GoButtonClick);

            _hidePos = -_startButton.transform.position.y;
        }

        private void GoButtonClick()
        {
            _canvasGroup.blocksRaycasts = false;

            _startButton.transform.DOMoveY(_hidePos, _fadeDuration).SetEase(Ease.Linear).OnComplete(() =>
            {
                _gameCamera.ActiveGameCamera();
                _gameMode.PrepareToStart().Forget();

                gameObject.SetActive(false);
            });
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveListener(GoButtonClick);
        }
    }
}