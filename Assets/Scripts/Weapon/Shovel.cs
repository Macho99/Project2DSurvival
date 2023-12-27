using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : Weapon
{
    [Space(20)]
    [SerializeField] private float scale = 1.5f;
    [SerializeField] private float angle = 50f;
    [SerializeField] private float angularVelocity = 360f;

    [SerializeField] private ShovelStatPerLevel statPerLevel;

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
        pivot.transform.Rotate(new Vector3(0, 0, -angle));

        child.SetStat((int)(damage + floatErrorCorrector), knockBackForce, (int)(maxMonsterHits + floatErrorCorrector));

        pivot.gameObject.SetActive(true);

        _ = StartCoroutine(CoMove(angle, angularVelocity));
    }

    IEnumerator CoMove(float angle, float angularVelocity)
    {
        yield return null;

        float curAngle = angle;

        while (curAngle > -angle)
        {
            float rotateAmount = angularVelocity * Time.unscaledDeltaTime;
            curAngle -= rotateAmount;
            pivot.transform.Rotate(new Vector3(0, 0, rotateAmount));
            yield return null;
        }

        pivot.gameObject.SetActive(false);
    }

    public override void LevelUp()
    {
        if (IsMaxLevel()) return;

        WeaponLevelUp(statPerLevel);
        scale           += statPerLevel.scale;
        angle           += statPerLevel.angle;
        angularVelocity += statPerLevel.angularVelocity;
    }
}