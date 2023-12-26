using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDelay : WeaponState
{
    public StateDelay(Weapon weapon) : base(weapon)
    {
    }

    public override void Update()
    {
        owner.SubDelay(Time.deltaTime);
        if (owner.IsDelayPassed())
        {
            owner.ChangeState(WeaponStateType.Ready);
        }
    }

    public override void Use()
    {
        //nothing
    }
}
