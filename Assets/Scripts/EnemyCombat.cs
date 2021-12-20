using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(Collider2D))]
public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float _punchForce;
    [SerializeField] private float _damage;
    [SerializeField] private string _animationDeathTrigger;

    private Animator _animator;
    private EnemyMovement _movement;

    private void Start()
    {
        _movement = GetComponent<EnemyMovement>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth player;

        if (collision.collider.TryGetComponent<PlayerHealth>(out player))
        {
            if (collision.contacts[0].normal != Vector2.down)
            {
                float deltaPositionX = collision.transform.position.x - transform.position.x;
                player.GetComponent<PlayerMovement>().TakePunch(deltaPositionX, _punchForce);
                player.TakeDamage(_damage);
            }
            else
            {
                Die();
            }
        }
    }

    private void Die()
    {
        _animator.SetTrigger(_animationDeathTrigger);
        GetComponent<Collider2D>().enabled = false;
        _movement.enabled = false;
        this.enabled = false;
    }
}
