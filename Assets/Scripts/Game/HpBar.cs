using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image hpView;

        public void ChangeHpView(float currentHp, float maxHp)
        {
            float normalizedHp = Mathf.Clamp01(currentHp / maxHp);
            hpView.fillAmount = normalizedHp;
        }
    }
}