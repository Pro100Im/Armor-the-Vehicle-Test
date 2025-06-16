using UnityEngine;

namespace Game.Car
{
    public class CarHp : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        [Space]
        [SerializeField] private float _maxHp;

        private float _currentHp;

        private void Awake()
        {
            _currentHp = _maxHp;
        }

        public void Show()
        {
            _currentHp = _maxHp;
            _hpBar.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _hpBar.gameObject.SetActive(false);
        }

        public bool IsAlive() => _currentHp > 0;

        public void TakeDamage(float damage)
        {
            _currentHp -= damage;
            _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);

            _hpBar.ChangeHpView(_currentHp, _maxHp);
        }
    }
}