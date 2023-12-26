using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponState
{
    protected Weapon owner;

    public WeaponState(Weapon weapon)
    {
        owner = weapon;
    }

    public abstract void Use();
    public abstract void Update();
}
