using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth _referencePlayer;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _updatingTime = 1f;

    private void OnEnable()
    {
        _referencePlayer.AddChangeHealthHandler(UpdateValue);
    }

    public void UpdateValue()
    {
        float value = _referencePlayer.CurrentHealth / _referencePlayer.MaxHealth;
        float delta = value - _slider.value;
        DOTween.To(() => _slider.value, x => _slider.value = x, value, _updatingTime);
    }

    private void OnDisable()
    {
        _referencePlayer?.RemoveChangeHealthHandler(UpdateValue);
    }
}
