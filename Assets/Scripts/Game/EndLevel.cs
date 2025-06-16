using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class EndLevel : MonoBehaviour
    {
        [field: SerializeField] public Button NextButton { get; private set; }
        [field: SerializeField] public Button RestartButton { get; private set; }

        [SerializeField] private CanvasGroup _winScreen;
        [SerializeField] private CanvasGroup _loseScreen;
        [Space]
        [SerializeField] private float fadeDuration = 0.5f;

        private void Awake()
        {
            NextButton.onClick.AddListener(Next);
        }

        public void LoseLevel()
        {
            _loseScreen.DOFade(1, fadeDuration);
            _loseScreen.blocksRaycasts = true;
        }

        public void WinLevel()
        {
            _winScreen.DOFade(1, fadeDuration);
            _winScreen.blocksRaycasts = true;
        }

        private void Next()
        {
            _winScreen.DOFade(0, fadeDuration);
            _winScreen.blocksRaycasts = false;
        }

        private void OnDestroy()
        {
            NextButton.onClick.RemoveAllListeners();
            RestartButton.onClick.RemoveAllListeners();
        }
    }
}