using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth _showingStats;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _updatingTime = 1f;

    private void OnEnable()
    {
        _showingStats.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        float value = _showingStats.CurrentHealth / _showingStats.MaxHealth;
        DOTween.To(() => _slider.value, x => _slider.value = x, value, _updatingTime);
    }

    private void OnDisable()
    {
        _showingStats.HealthChanged -= OnHealthChanged;
    }
}
