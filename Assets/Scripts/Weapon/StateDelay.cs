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
        float time;
        if (owner.IsUseRealTime())
        {
            //스킬 선택 창 등 시간을 임의로 멈추었을 때
            if(Time.timeScale < 0.001f)
            {
                time = Time.deltaTime;
            }
            else
            {
                time = Time.unscaledDeltaTime;
            }
        }
        else { 
            time = Time.deltaTime;
        }

        owner.SubDelay(time);

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
