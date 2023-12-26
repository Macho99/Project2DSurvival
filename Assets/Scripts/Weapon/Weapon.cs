using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private bool useRealTime = false;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private bool useMagazine = false;
    [SerializeField] private int magazineSize = 0;
    [SerializeField] private float reloadTime = 3f;

    public Vector2 HandPos { get; protected set; }

    private SpriteRenderer spRenderer;
    private WeaponState state;
    private WeaponState[] states;
    private float curDelay;
    private int curMagazineSize;
    private float curReloadTime;

    protected Player player;
    protected Transform aimTrans;

    public bool IsUseRealTime()
    {
        return useRealTime;
    }

    public void SubDelay(float amount)
    {
        curDelay -= amount;
        if(curDelay < 0f)
        {
            curDelay = 0f;
        }
        player.DelayChanged((delay - curDelay) / delay);
    }

    public bool IsDelayPassed()
    {
        if(curDelay <= 0f)
        {
            return true;
        }
        return false;
    }

    public void SubReloadTime(float amount)
    {
        curReloadTime -= amount;
        if(curReloadTime < 0f)
        {
            curReloadTime = 0f;
        }
        player.ReloadTimeChanged((reloadTime - curReloadTime) / reloadTime);
    }

    public bool IsReloadEnd()
    {
        if (curReloadTime <= 0f)
            return true;
        return false;
    }

    public void SetCurMagazine()
    {
        curMagazineSize = magazineSize;
    }

    public bool IsUseMagazine()
    {
        return useMagazine;
    }

    public void SubMagazineSize()
    {
        if (!useMagazine) return;

        curMagazineSize--;
    }

    public bool IsTimeToReload()
    {
        if (!useMagazine) return false;

        if(curMagazineSize <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetCurReloadTime()
    {
        curReloadTime = reloadTime;
    }

    public void SetCurDelay()
    {
        curDelay = delay;
    }

    public void ChangeState(WeaponStateType type)
    {
        state = states[(int)type];
    }

    protected virtual void Awake()
    {
        spRenderer = GetComponentInChildren<SpriteRenderer>();

        states = new WeaponState[(int)WeaponStateType.Size];

        states[0] = new StateReady(this);
        states[1] = new StateDelay(this);
        states[2] = new StateReload(this);

        curMagazineSize = magazineSize;

        state = states[0];
    }

    protected virtual void Start()
    {
        player = PlaySceneMaster.Instance.Player;
        aimTrans = player.GetAimTransform();
    }

    protected virtual void Update()
    {
        state.Update();
    }

    public void FlipX(bool isFlip)
    {
        if (isFlip)
        {
            spRenderer.gameObject.transform.localPosition = new Vector2(-HandPos.x, HandPos.y);
            spRenderer.flipX = true;
        }
        else
        {
            spRenderer.gameObject.transform.localPosition = HandPos;
            spRenderer.flipX = false;
        }
    }

    public void Use()
    {
        state.Use();
    }

    public abstract void Effect();
}
