using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExpLevel { Lv0, Lv1, Lv2};

public class ExpCoin : MonoBehaviour
{
    [SerializeField] Sprite coinSprite0;
    [SerializeField] Sprite coinSprite1;
    [SerializeField] Sprite coinSprite2;

    private SpriteRenderer spRenderer;
    private int expAmount;

    private void Awake()
    {
        spRenderer = GetComponent<SpriteRenderer>();
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
            Destroy(gameObject);
        }
    }
}