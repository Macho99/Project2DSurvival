using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Hand : MonoBehaviour
{
    private int curWeaponIdx;
    private Weapon[] weapons;

    [SerializeField] private UnityEvent onWeaponChange;

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

    public void FlipX(bool isFlip)
    {
        weapons[curWeaponIdx]?.FlipX(isFlip);
    }

    public void OnNumPressed(int num)
    {
        weapons[curWeaponIdx]?.gameObject.SetActive(false);

        curWeaponIdx = num - 1;

        weapons[curWeaponIdx]?.gameObject.SetActive(true);

        onWeaponChange?.Invoke();
    }

    public void CurWeaponLevelUp()
    {
        weapons[curWeaponIdx]?.LevelUp();
    }
}
