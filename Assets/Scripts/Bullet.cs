using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int damage = 5;

    private BoxCollider2D col;
    
    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Monster>(out Monster monster))
        {
            monster.TakeDamage(damage);
            monster.KnockBack(transform.up, 5f);
        }
    }
}