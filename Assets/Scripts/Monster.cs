using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Monster : MonoBehaviour
{
    [SerializeField] private float attackDist = 1f;
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float curHp = 10;
    [SerializeField] private float maxHp = 10;

    private BoxCollider2D col;
    private Rigidbody2D rb;
    private SpriteRenderer spRenderer;
    private Animator anim;
    private Coroutine moveCoroutine;
    private Transform playerTrans;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        spRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        playerTrans = PlaySceneMaster.Instance.Player.transform;
        moveCoroutine = StartCoroutine(CoMove());
    }

    private void FixedUpdate()
    {
        if(rb.velocity.x < 0f)
        {
            spRenderer.flipX = true;
        }
        else if(rb.velocity.x > 0f)
        {
            spRenderer.flipX = false;
        }
    }

    private IEnumerator CoMove()
    {
        while (true)
        {
            if (IsInAttackDist())
            {
                yield return StartCoroutine(CoAttack());
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            {
                yield return new WaitUntil(() => !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"));
            }

            Vector2 dir = playerTrans.position - transform.position;
            dir.Normalize();

            rb.velocity = dir * moveSpeed;

            yield return null;
        }
    }

    private bool IsInAttackDist()
    {
        if((playerTrans.position - transform.position).sqrMagnitude < attackDist * attackDist)
        {
            return true;
        }

        return false;
    }

    protected virtual IEnumerator CoAttack()
    {
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDelay);
    }

    public void KnockBack(Vector2 dir, float force)
    {
        StartCoroutine(CoKnockBack(dir, force));
    }

    private IEnumerator CoKnockBack(Vector2 dir, float force)
    {
        yield return new WaitUntil(() => {
            return anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")
                || anim.GetCurrentAnimatorStateInfo(0).IsName("Dead");});
        rb.AddForce(dir * force, ForceMode2D.Impulse);
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;

        if (curHp <= 0)
        {
            curHp = 0;
            Die();
        }
        else
        {
            anim.SetTrigger("Hit");
        }
    }

    private void Die()
    {
        anim.SetTrigger("Dead");
        col.enabled = false;
        StopCoroutine(moveCoroutine);
    }
}