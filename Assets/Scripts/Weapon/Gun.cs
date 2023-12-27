using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [Space(20)]
    [Tooltip("0 ~ 1사이의 값.\n 0일 때 가장 정확\n 1일 때 180도까지 오차 발생")]
    [SerializeField] private float accuracy = 0.5f;
    [SerializeField] private float bulletSpeed = 5f;

    [Space(20)]
    [SerializeField] private float numOfBulletPerShot = 1f; //int
    [SerializeField] private float shotAngle = 5f;

    [SerializeField] private GunStatePerLevel statPerLevel;

    protected override void Awake()
    {
        base.Awake();
        HandPos = new Vector2(-0.18f, -0.29f);
    }

    public override void Effect()
    {
        for(int i = 0; i < numOfBulletPerShot; i++)
        {
            Bullet bullet = ObjPool.Instance.AllocateObj(ObjPoolType.Bullet).GetComponent<Bullet>();
            SetBulletDirectionWithOffset(bullet, i + 1);
        }
    }

    private void SetBulletDirectionWithOffset(Bullet bullet, int idx)
    {
        float maxOffsetAngle = Mathf.LerpAngle(0f, 90f, accuracy);
        float offsetAngle = Random.Range(-maxOffsetAngle, maxOffsetAngle);

        Vector3 euler = aimTrans.rotation.eulerAngles;
        euler.z += offsetAngle;

        float curShotAngle = (idx / 2) * shotAngle;

        if(idx % 2 == 1)
        {
            curShotAngle = -curShotAngle;
        }

        euler.z += curShotAngle;

        bullet.Init((int) (damage + floatErrorCorrector), knockBackForce, bulletSpeed, (int) (maxMonsterHits + floatErrorCorrector)
            , aimTrans.position, Quaternion.Euler(euler));
    }

    public override void LevelUp()
    {
        if (IsMaxLevel()) return;

        WeaponLevelUp(statPerLevel);
        accuracy         += statPerLevel.accuracy;
        bulletSpeed      += statPerLevel.bulletSpeed;
        numOfBulletPerShot += statPerLevel.numOfBulletPerShot;
        shotAngle        += statPerLevel.shotAngle;
    }
}