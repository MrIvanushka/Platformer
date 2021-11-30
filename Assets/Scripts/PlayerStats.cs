using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMovement))]
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private GameObject _deathMessage;

    private float _currentHealth;
    private PlayerMovement _movement;
    
    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        ChangeHealth(_maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
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
    }

    public void Heal(float healValue)
    {
        if (healValue > 0)
        {
            float newHealth = _currentHealth + healValue;

            if (newHealth > _maxHealth)
                ChangeHealth(_maxHealth);
            else
                ChangeHealth(newHealth);
        }
    }

    public void TakePunch(float deltaPositionX, float punchForce, float damage)
    {
        TakeDamage(damage);
        _movement.TakePunch(deltaPositionX, punchForce);
    }

    private void ChangeHealth(float health)
    {
        _currentHealth = health;
        _healthBar.UpdateValue(_currentHealth / _maxHealth);
    }

    private void Die()
    {
        _deathMessage.SetActive(true);
        Time.timeScale = 0;
    }
}
