using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Hand : MonoBehaviour
{
    [SerializeField] private UnityEvent onWeaponChange;

    private int curWeaponIdx;
    private Weapon[] weapons;
    private bool curFlip;

    private void Awake()
    {
        weapons = new Weapon[3];
        Weapon[] tmpWeapons = GetComponentsInChildren<Weapon>();

        for(int i = 0; i < Mathf.Min(weapons.Length, tmpWeapons.Length); i++)
        {
            weapons[i] = tmpWeapons[i];
            weapons[i].gameObject.SetActive(false);
        }

        curWeaponIdx = 0;
        weapons[curWeaponIdx]?.gameObject.SetActive(true);
    }

    private void Start()
    {
        StartCoroutine(CoStart());
    }

    private IEnumerator CoStart()
    {
        yield return null;
        onWeaponChange?.Invoke();
    }

    public int GetCurWeaponIdx()
    {
        return curWeaponIdx;
    }

    public Sprite[] GetWeaponSprite()
    {
        Sprite[] sprites = new Sprite[weapons.Length];
        for(int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == null)
                sprites[i] = null;
            else
            {
                sprites[i] = weapons[i].GetBoxedSprite();
            }
        }

        return sprites;
    }

    public void Fire()
    {
        weapons[curWeaponIdx]?.Use();
    }

    public void FlipX(bool flip)
    {
        curFlip = flip;
        weapons[curWeaponIdx]?.FlipX(flip);
    }

    public void OnNumPressed(int num)
    {
        weapons[curWeaponIdx]?.gameObject.SetActive(false);

        curWeaponIdx = num - 1;

        weapons[curWeaponIdx]?.gameObject.SetActive(true);
        weapons[curWeaponIdx]?.FlipX(curFlip);

        onWeaponChange?.Invoke();
        PlaySceneMaster.Instance.Player.CurMagazineSizeChange();
    }

    public void CurWeaponLevelUp()
    {
        weapons[curWeaponIdx]?.LevelUp();
        onWeaponChange?.Invoke();
    }

    public void WeaponLevelUp(string name)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i]?.GetName() == name)
            {
                weapons[i].LevelUp();
                onWeaponChange?.Invoke();
            }
        }
    }

    public string GetCurWeaponName()
    {
        return weapons[curWeaponIdx]?.GetName();
    }

    public int? GetCurWeaponLevel()
    {
        return weapons[curWeaponIdx]?.GetLevel();
    }

    public int? GetLeftMagazineSize()
    {
        return weapons[curWeaponIdx]?.GetLeftMagazineSize();
    }

    public bool HaveWeapon(string name)
    {
        for(int i=0;i<weapons.Length;i++)
        {
            if (weapons[i]?.GetName() == name)
            {
                return true;
            }
        }
        return false;
    }

    public void ReplaceWeapon(Weapon newWeapon)
    {
        if (weapons[curWeaponIdx] != null)
        {
            Destroy(weapons[curWeaponIdx].gameObject);
        }
        weapons[curWeaponIdx] = newWeapon;
        newWeapon.transform.parent = transform;
        newWeapon.transform.localPosition = Vector3.zero;
        onWeaponChange?.Invoke();
    }
}
