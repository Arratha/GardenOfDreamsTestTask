using System;

using UnityEngine;


namespace Units.Fight
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        private int _currentHealth;

        public event Action<float> OnTakeDamage;
        public event Action OnDie;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            OnTakeDamage?.Invoke((float)_currentHealth / _maxHealth);

            if (_currentHealth <= 0)
                OnDie?.Invoke();
        }
    }
}