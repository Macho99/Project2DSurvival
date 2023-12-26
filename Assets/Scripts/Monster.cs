using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Monster : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float attackDist = 1f;
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int curHp = 10;
    [SerializeField] private int maxHp = 10;
    [SerializeField] private float returnDuration = 5f;
    [SerializeField] private ExpCoin expCoinPrefab;

    private BoxCollider2D col;
    private Rigidbody2D rb;
    private SpriteRenderer[] spRenderers;
    private Animator anim;
    private Coroutine moveCoroutine;
    private Player player;
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
        player = PlaySceneMaster.Instance.Player;
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

            Vector2 dir = player.transform.position - transform.position;
            dir.Normalize();

            rb.velocity = dir * moveSpeed;

            yield return null;
        }
    }

    private bool IsInAttackDist()
    {
        if((player.transform.position - transform.position).sqrMagnitude < attackDist * attackDist)
        {
            return true;
        }

        return false;
    }

    protected virtual IEnumerator CoAttack()
    {
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Attack");
        player.TakeDamage(damage);
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
        if(curHp <= 0) return; //공격 받기도 전에 이미 죽어있으면 return

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
        _ = GameManager.Data;

        ExpCoin expCoin = Instantiate(expCoinPrefab);
        expCoin.Init(ExpLevel.Lv0, 5);
        expCoin.transform.position = transform.position + Random.insideUnitSphere * 0.5f;

        player.KillMonster();
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