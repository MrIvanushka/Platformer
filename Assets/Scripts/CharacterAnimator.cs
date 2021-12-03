using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private string _velocityXLiteral;
    [SerializeField] private string _velocityYLiteral;

    private Animator _animator;
    private PlayerMovement _movement;
    private bool _speedIsPositive;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
        _speedIsPositive = true;
    }

    private void Update()
    {
        Vector2 speed = _movement.Velocity;
        _animator.SetFloat(_velocityXLiteral, Mathf.Abs(speed.x));
        _animator.SetFloat(_velocityYLiteral, speed.y);

        if (speed.x != 0 && _speedIsPositive != speed.x > 0)
        {
            _speedIsPositive = !_speedIsPositive;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
