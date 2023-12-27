using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurWeaponState : MonoBehaviour
{
    [SerializeField] Image weapon1;
    [SerializeField] Image weapon2;
    [SerializeField] Image weapon3;
    [SerializeField] Sprite noneSprite;
    [SerializeField] Color curWeaponColor;
    [SerializeField] Color otherWeaponColor;
    [SerializeField] TextMeshProUGUI weaponSummary;
    [SerializeField] GameObject leftAmmoInfo;
    [SerializeField] TextMeshProUGUI leftAmmoText;

    private Image[] weapons;

    private Hand hand;

    private void Awake()
    {
        weapons = new Image[3];
        weapons[0] = weapon1;
        weapons[1] = weapon2;
        weapons[2] = weapon3;
    }

    private void Start()
    {
        hand = PlaySceneMaster.Instance.Player.GetHand();
    }

    public void WeaponChange()
    {
        if(hand == null)
        {
            print("!!");
        }
        Sprite[] sprites = hand.GetWeaponSprite();

        for (int i = 0; i < weapons.Length; i++)
        {
            if (sprites[i] == null)
            {
                weapons[i].sprite = noneSprite;
            }
            else
            {
                weapons[i].sprite = sprites[i];
            }
            weapons[i].color = otherWeaponColor;
        }

        int curWeaponIdx = hand.GetCurWeaponIdx();
        weapons[curWeaponIdx].color = curWeaponColor;

        string curWeaponName = hand.GetCurWeaponName();
        if(curWeaponName == null)
        {
            weaponSummary.text = "";
        }
        else
        {
            weaponSummary.text = $"{curWeaponName} Lv.{hand.GetCurWeaponLevel()}";
        }
    }

    public void LeftAmmoChange()
    {
        int? num = hand.GetLeftMagazineSize();
        if(num == null)
        {
            leftAmmoInfo.SetActive(false);
        }
        else
        {
            leftAmmoInfo.SetActive(true);
            leftAmmoText.text = $"X {num}";
        }
    }
}
