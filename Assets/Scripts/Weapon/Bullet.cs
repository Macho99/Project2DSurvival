using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float returnTime = 5f;


    private int damage;
    private float knockBackForce;
    private float moveSpeed;
    private int leftMonsterHits;

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

    public void Init(int damage, float knockBackForce, float moveSpeed, int maxMonsterHits, Vector2 pos, Quaternion quaternion)
    {
        this.damage = damage;
        this.knockBackForce = knockBackForce;
        this.moveSpeed = moveSpeed;
        this.leftMonsterHits = maxMonsterHits;

        transform.position = pos;
        transform.rotation = quaternion;
        rb.velocity = transform.up * moveSpeed;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Monster>(out Monster monster))
        {
            monster.TakeDamage(damage, transform.up, knockBackForce);
            leftMonsterHits--;
            if(leftMonsterHits <= 0)
            {
                ObjPool.Instance.ReturnObj(ObjPoolType.Bullet, gameObject);
            }
        }
    }

    private IEnumerator CoReturn()
    {
        yield return new WaitForSeconds(returnTime);
        ObjPool.Instance.ReturnObj(ObjPoolType.Bullet, gameObject);
    }
}