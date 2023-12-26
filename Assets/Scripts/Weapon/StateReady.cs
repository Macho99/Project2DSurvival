using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateReady : WeaponState
{
    public StateReady(Weapon weapon) : base(weapon)
    {
    }

    public override void Update()
    {
        //nothing
    }

    public override void Use()
    {
        owner.Effect();
        owner.SubMagazineSize();
        if (owner.IsTimeToReload())
        {
            owner.SetCurReloadTime();
            owner.ChangeState(WeaponStateType.Reload);
        }
        else
        {
            owner.SetCurDelay();
            owner.ChangeState(WeaponStateType.Delay);
        }
    }
}