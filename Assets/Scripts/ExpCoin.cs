using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExpLevel { Lv0, Lv1, Lv2};

[RequireComponent(typeof(Rigidbody2D))]
public class ExpCoin : MonoBehaviour
{
    [SerializeField] Sprite coinSprite0;
    [SerializeField] Sprite coinSprite1;
    [SerializeField] Sprite coinSprite2;
    [SerializeField] float magnetBackForce = 5f;
    [SerializeField] float magnetSpeed = 3f;

    private bool hit;
    private Rigidbody2D rb;
    private SpriteRenderer spRenderer;
    private int expAmount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        hit = false;
    }

    public void Init(ExpLevel level, int amount)
    {
        switch (level)
        {
            case ExpLevel.Lv0:
                spRenderer.sprite = coinSprite0;
                break;
            case ExpLevel.Lv1: 
                spRenderer.sprite = coinSprite1;
                break;
            case ExpLevel.Lv2: 
                spRenderer.sprite = coinSprite2;
                break;
        }
        expAmount = amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            player.AddExp(expAmount);
            ObjPool.Instance.ReturnObj(ObjPoolType.Coin, gameObject);
        }
        else if(collision.TryGetComponent<Magnet>(out _))
        {
            if(!hit)
                MagnetHitted();
        }
    }

    public void MagnetHitted()
    {
        hit = true;
        if (gameObject.activeSelf)
        {
            _ = StartCoroutine(CoMoveToPlayer());
        }
    }

    private IEnumerator CoMoveToPlayer()
    {
        Player player = PlaySceneMaster.Instance.Player;
        Vector2 dir = (transform.position - player.transform.position).normalized;
        rb.AddForce(dir * magnetBackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            dir = (player.transform.position - transform.position).normalized;
            rb.velocity = dir * magnetSpeed;
            yield return null;
        }
    }

    private void OnDisable()
    {
        hit = false;
    }
}