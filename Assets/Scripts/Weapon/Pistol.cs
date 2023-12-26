﻿using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    protected override void Awake ()
    {
        base.Awake();
        HandPos = new Vector2(-0.18f, -0.29f);
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Use()
    {
        Bullet bullet = ObjPool.Instance.AllocateObj(ObjPoolType.Bullet).GetComponent<Bullet>();
        bullet.SetDirection(aimTrans.position, aimTrans.rotation);
    }
}