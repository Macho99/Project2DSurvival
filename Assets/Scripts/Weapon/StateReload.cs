using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateReload : WeaponState
{
    public StateReload(Weapon weapon) : base(weapon)
    {
    }

    public override void Update()
    {
        owner.SubReloadTime(Time.deltaTime);
        if (owner.IsReloadEnd())
        {
            owner.SetCurMagazine();
            owner.ChangeState(WeaponStateType.Ready);
        }
    }

    public override void Use()
    {
        //nothing
    }
}
