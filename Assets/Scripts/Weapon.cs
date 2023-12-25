using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Vector2 HandPos { get; protected set; }

    private SpriteRenderer spRenderer;

    protected Player player;
    protected Transform aimTrans;

    protected virtual void Awake()
    {
        spRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        player = PlaySceneMaster.Instance.Player;
        aimTrans = player.GetAimTransform();
    }

    protected virtual void Update()
    {

    }

    public void FlipX(bool isFlip)
    {
        if (isFlip)
        {
            spRenderer.gameObject.transform.localPosition = new Vector2(-HandPos.x, HandPos.y);
            spRenderer.flipX = true;
        }
        else
        {
            spRenderer.gameObject.transform.localPosition = HandPos;
            spRenderer.flipX = false;
        }
    }

    public abstract void Use();
}
