using UnityEngine;
using UnityEngine.UI;

using Units.Fight;


namespace UI.Units
{ 
    public class HealthbarController : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;

        [Space(10)]
        [Header("Optional")]
        [SerializeField] private Health _health;

        private void Awake()
        {
            TryInitialize(_health);
        }

        public void TryInitialize(Health health)
        {
            if (health == null)
                return;

            _health = health;

            _healthBar.fillAmount = 1;

            _health.OnTakeDamage += ChangeHealth;
        }

        private void ChangeHealth(float healthPercent)
        {
            _healthBar.fillAmount = healthPercent;
        }

        private void OnDestroy()
        {
            _health.OnTakeDamage -= ChangeHealth;
        }
    }
}