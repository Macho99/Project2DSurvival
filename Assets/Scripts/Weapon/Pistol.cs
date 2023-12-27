using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private float accuracy = 0f;
    protected override void Awake ()
    {
        base.Awake();
        HandPos = new Vector2(-0.18f, -0.29f);
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Effect()
    {
        Bullet bullet = ObjPool.Instance.AllocateObj(ObjPoolType.Bullet).GetComponent<Bullet>();
        bullet.SetDirection(aimTrans.position, aimTrans.rotation);
    }
}