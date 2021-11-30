using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _updatingTime = 1f; 

    public void UpdateValue(float value)
    {
        float delta = value - _slider.value;
        DOTween.To(() => _slider.value, x => _slider.value = x, value, _updatingTime);
    }
}
