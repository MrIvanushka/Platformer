using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private enum Direction
    {
        Left,
        Right
    };

    private Animator _animator;
    private PlayerMovement _movement;
    private Direction _direction;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
        _direction = Direction.Right;
    }

    void Update()
    {
        Vector2 speed = _movement.Velocity;
        _animator.SetFloat("VelocityX", Mathf.Abs(speed.x));
        _animator.SetFloat("VelocityY", speed.y);

        if (speed.x != 0)
        {
            Direction newDirection = ScoreCurrentDirection(speed.x);
            if (newDirection != _direction)
            {
                _direction = newDirection;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private Direction ScoreCurrentDirection(float speed)
    {
        if (speed > 0)
            return Direction.Right;
        else
            return Direction.Left;
    }
}
