using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float returnTime = 5f;
    [SerializeField] private float knockBackForce = 5f;

    private BoxCollider2D col;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _ = StartCoroutine(CoReturn());
    }

    public void SetDirection(Vector2 pos, Quaternion quaternion)
    {
        transform.position = pos;
        transform.rotation = quaternion;
        rb.velocity = transform.up * moveSpeed;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Monster>(out Monster monster))
        {
            monster.TakeDamage(damage, transform.up, knockBackForce);
        }
    }

    private IEnumerator CoReturn()
    {
        yield return new WaitForSeconds(returnTime);
        ObjPool.Instance.ReturnObj(ObjPoolType.Bullet, gameObject);
    }
}