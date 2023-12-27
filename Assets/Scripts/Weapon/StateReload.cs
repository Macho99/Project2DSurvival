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
        float time;
        if (owner.IsUseRealTime())
        {
            //��ų ���� â �� �ð��� ���Ƿ� ���߾��� ��
            if (Time.timeScale < 0.001f)
            {
                time = Time.deltaTime;
            }
            else
            {
                time = Time.unscaledDeltaTime;
            }
        }
        else
        {
            time = Time.deltaTime;
        }

        owner.SubReloadTime(time);

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
