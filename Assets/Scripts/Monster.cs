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
    [SerializeField] private int curHp = 10;
    [SerializeField] private int maxHp = 10;
    [SerializeField] private float returnDuration = 5f;

    private BoxCollider2D col;
    private Rigidbody2D rb;
    private SpriteRenderer[] spRenderers;
    private Animator anim;
    private Coroutine moveCoroutine;
    private Transform playerTrans;
    private Sprite initSprite;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        spRenderers = GetComponentsInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        initSprite = spRenderers[0].sprite;
    }

    private void Start()
    {
        playerTrans = PlaySceneMaster.Instance.Player.transform;
    }

    private void OnEnable()
    {
        moveCoroutine = StartCoroutine(CoMove());
    }

    private void FixedUpdate()
    {
        if(rb.velocity.x < 0f)
        {
            spRenderers[0].flipX = true;
        }
        else if(rb.velocity.x > 0f)
        {
            spRenderers[0].flipX = false;
        }
    }

    private IEnumerator CoMove()
    {
        yield return null;
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

    public void TakeDamage(int damage, Vector2 dir, float force)
    {
        TakeDamage(damage);
        StartCoroutine(CoKnockBack(dir, force));
    }

    private void Die()
    {
        anim.SetBool("Dead", true);
        col.enabled = false;
        StopCoroutine(moveCoroutine);
        StartCoroutine(CoReturnObj());
    }

    private void OnDisable()
    {
        anim.SetBool("Dead", false);
        foreach(SpriteRenderer sr in spRenderers)
        {
            Color color = sr.color;
            color.a = 1f;
            sr.color = color;
        }
        spRenderers[0].sprite = initSprite;
        col.enabled = true;
        curHp = maxHp;
    }

    private IEnumerator CoReturnObj()
    {
        yield return new WaitForSeconds(returnDuration);
        ObjPool.Instance.ReturnObj(ObjPoolType.Monster, gameObject);
    }
}