using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game
{
    public class StartTutorial : MonoBehaviour
    {
        [SerializeField] private InputAction tapAction;
        [Space]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _tutorialView;
        [SerializeField] private float _targetAimPos = 140;
        [SerializeField] private float _animSpeed = 0.5f;

        public void Show()
        {
            tapAction.Enable();

            _canvasGroup.DOFade(1, _animSpeed);
            _canvasGroup.blocksRaycasts = true;
            _tutorialView.rectTransform.DOAnchorPosX(_targetAimPos, _animSpeed).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }

        public async UniTask TutorialProcess()
        {
            await UniTask.WaitUntil(() => tapAction.WasPerformedThisFrame());
            tapAction.Disable();

            Hide();
        }

        private void Hide()
        {
            DOTween.Kill(_tutorialView.rectTransform);
            gameObject.SetActive(false);
        }
    }
}