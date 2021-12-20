using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private GameObject _deathMessage;
    
    private float _currentHealth;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public event UnityAction HealthChanged;

    private void Start()
    {
        ChangeHealth(_maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            throw new Exception("Negative damage");
        
        float newHealth = _currentHealth - damage;

        if (newHealth < 0)
        {
            ChangeHealth(0);
            Die();
        }
        else
        {
            ChangeHealth(newHealth);
        }
    }

    public void Heal(float healValue)
    {
        if (healValue <= 0)
            throw new ArgumentOutOfRangeException("Negative healing value");

        float newHealth = _currentHealth + healValue;

        if (newHealth > _maxHealth)
            ChangeHealth(_maxHealth);
        else
            ChangeHealth(newHealth);
    }

    private void ChangeHealth(float health)
    {
        _currentHealth = health;
        HealthChanged?.Invoke();
    }

    private void Die()
    {
        _deathMessage.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        HealthChanged = null;
    }
}
