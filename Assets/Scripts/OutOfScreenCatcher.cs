using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class OutOfScreenCatcher : MonoBehaviour
{
    [SerializeField] private Transform _spawnpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerHealth>(out PlayerHealth player))
        {
            collision.transform.position = _spawnpoint.position;
        }
    }
}
