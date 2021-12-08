using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private GameObject _deathMessage;
    
    private float _currentHealth;
    private PlayerMovement _movement;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public event UnityAction OnChangeHealth;

    private void Start()
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
        else
        {
            throw new Exception("Negative damage");
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
        else
        {
            throw new Exception("Negative healing value");
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
        OnChangeHealth?.Invoke();
    }

    private void Die()
    {
        _deathMessage.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        OnChangeHealth = null;
    }
}
