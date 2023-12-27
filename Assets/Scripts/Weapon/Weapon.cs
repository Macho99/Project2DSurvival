using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string weaponName;
    [Multiline] 
    [SerializeField] private string weaponDescription;
    [SerializeField] private Sprite boxedSprite;
    [SerializeField] private bool useRealTime = false;
    [SerializeField] protected float damage = 5f;        //int
    [SerializeField] protected float knockBackForce = 5f;
    [SerializeField] protected float maxMonsterHits = 5f;//int

    [Space(20)]
    [SerializeField] protected float delay = 0.5f;

    [Space(20)]
    [SerializeField] private bool useMagazine = false;
    [SerializeField] protected float magazineSize = 0f;  //int
    [SerializeField] protected float reloadTime = 3f;

    protected float floatErrorCorrector = 0.001f;

    protected int maxLevel = 8;
    protected int level = 1;

    public Vector2 HandPos { get; protected set; }

    private SpriteRenderer spRenderer;
    private WeaponState state;
    private WeaponState[] states;
    private float curDelay;
    private int curMagazineSize;
    private float curReloadTime;

    protected Player player;
    protected Transform aimTrans;

    protected virtual void Awake()
    {
        spRenderer = GetComponentInChildren<SpriteRenderer>();

        states = new WeaponState[(int)WeaponStateType.Size];

        states[0] = new StateReady(this);
        states[1] = new StateDelay(this);
        states[2] = new StateReload(this);

        curMagazineSize = (int)(magazineSize + floatErrorCorrector);

        state = states[0];
    }

    public int GetLevel()
    {
        return level;
    }

    public int? GetLeftMagazineSize()
    {
        if (useMagazine)
        {
            return curMagazineSize;
        }
        else
        {
            return null;
        }
    }

    public string GetName()
    {
        return weaponName;
    }

    public string GetDescription()
    {
        return weaponDescription;
    }

    protected void WeaponLevelUp(WeaponStatPerLevel statPerLevel)
    {
        level++;
        damage          += statPerLevel.damage;
        knockBackForce  += statPerLevel.knockBackForce;
        maxMonsterHits  += statPerLevel.maxMonsterHits;
        delay           += statPerLevel.delay;
        magazineSize    += statPerLevel.magazineSize;
        reloadTime      += statPerLevel.reloadTime;
    }
    public abstract void LevelUp();

    public Sprite GetBoxedSprite()
    {
        return boxedSprite;
    }

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
        curMagazineSize = (int) (magazineSize + floatErrorCorrector);
        player.CurMagazineSizeChange();
    }

    public bool IsUseMagazine()
    {
        return useMagazine;
    }

    public void SubMagazineSize()
    {
        if (!useMagazine) return;

        curMagazineSize--;

        player.CurMagazineSizeChange();
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


    protected virtual void Start()
    {
        player = PlaySceneMaster.Instance.Player;
        aimTrans = player.GetAimTransform();
    }

    protected virtual void Update()
    {
        state.Update();
    }

    public void FlipX(bool flip)
    {
        if (flip)
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

    protected bool IsMaxLevel()
    {
        if(level >= maxLevel)
        {
            return true;
        }
        return false;
    }

    public void Use()
    {
        state.Use();
    }

    public abstract void Effect();

}
