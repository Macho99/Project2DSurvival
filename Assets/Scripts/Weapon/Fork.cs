using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fork : Weapon
{
    [Space(20)]
    [SerializeField] private float scale = 1.5f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveDist = 10f;

    [SerializeField] private ForkStatPerLevel statPerLevel;

    private Transform pivot;
    private DamagableObject child;

    protected override void Awake()
    {
        base.Awake();
        HandPos = new Vector2(-0.18f, -0.29f);
        child = GetComponentInChildren<DamagableObject>(false);
        child.transform.localPosition = Vector2.up;
        pivot = child.transform.parent;
        pivot.gameObject.SetActive(false);
    }

    public override void Effect()
    {
        pivot.transform.localScale = Vector3.one * scale;
        pivot.transform.rotation = aimTrans.rotation;

        child.SetStat((int)(damage + floatErrorCorrector), knockBackForce, (int)(maxMonsterHits + floatErrorCorrector));

        pivot.gameObject.SetActive(true);

        _ = StartCoroutine(CoMove());
    }

    IEnumerator CoMove()
    {
        yield return null;

        float curDist = 0;

        while (curDist < moveDist)
        {
            float moved = moveSpeed * Time.unscaledDeltaTime;
            pivot.transform.Translate(Vector2.up * moved);
            curDist += moved;
            yield return null;
        }

        pivot.gameObject.SetActive(false);
        pivot.transform.localPosition = Vector3.zero;
    }

    public override void LevelUp()
    {
        if (IsMaxLevel()) return;
        WeaponLevelUp(statPerLevel);
        scale += statPerLevel.scale;
        moveSpeed += statPerLevel.moveSpeed;
        moveDist += statPerLevel.moveDist;
    }
}