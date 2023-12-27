using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DamagableObject : MonoBehaviour
{
    private BoxCollider2D col;

    private int damage;
    private float knockBackForce;

    private bool targetLimit;
    private int leftMonsterHits;
    private int maxMonsterHits;
    public void Init(int damage, float knockBackForce)
    {
        targetLimit = false;

        this.damage = damage;
        this.knockBackForce = knockBackForce;
    }

    public void SetStat(int damage, float knockBackForce, int maxMonsterHits)
    {
        targetLimit = true;

        this.damage = damage;
        this.knockBackForce = knockBackForce;
        this.maxMonsterHits = maxMonsterHits;
        this.leftMonsterHits = maxMonsterHits;
    }

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Monster>(out Monster monster))
        {
            if(targetLimit)
            {
                if (leftMonsterHits > 0)
                {
                    leftMonsterHits--;

                    HitMonster(monster);
                }
            }
            else
            {
                HitMonster(monster);
            }
        }
    }

    private void HitMonster(Monster monster)
    {
        Vector2 dir = monster.transform.position - PlaySceneMaster.Instance.Player.transform.position;
        dir.Normalize();
        monster.TakeDamage(damage, dir, knockBackForce);
    }

    private void OnDisable()
    {
        leftMonsterHits = maxMonsterHits;
    }
}
