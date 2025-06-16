using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private float _hpChangeDuration = 0.3f;
        [Space]
        [SerializeField] private Image _hpView;
        [SerializeField] private Image _hpBackView;

        public void ChangeHpView(float currentHp, float maxHp)
        {
            DOTween.Kill(_hpBackView);

            float normalizedHp = Mathf.Clamp01(currentHp / maxHp);

            _hpView.fillAmount = normalizedHp;
            _hpBackView.DOFillAmount(normalizedHp, _hpChangeDuration);
        }
    }
}