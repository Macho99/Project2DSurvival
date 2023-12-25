using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private Bullet bulletPrefab;

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
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = aimTrans.position;
        bullet.transform.rotation = aimTrans.rotation;
    }
}