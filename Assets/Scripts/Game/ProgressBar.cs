using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider _progressView;
        [SerializeField] private TextMeshProUGUI _progressValue;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void Show()
        {
            _canvasGroup.alpha = 1f;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0f;
        }

        public void SetProgress(float currentProgress, float goal)
        {
            float normalizedHp = Mathf.Clamp01(currentProgress / goal);

            _progressView.value = normalizedHp;
            _progressValue.text = currentProgress.ToString("F0");
        }
    }
}