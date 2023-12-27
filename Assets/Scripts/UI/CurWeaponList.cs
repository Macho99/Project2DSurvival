using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurWeaponList : MonoBehaviour
{
    [SerializeField] Image weapon1;
    [SerializeField] Image weapon2;
    [SerializeField] Image weapon3;
    [SerializeField] Sprite noneSprite;
    [SerializeField] Color curWeaponColor;
    [SerializeField] Color otherWeaponColor;

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

    public void UIUpdate()
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

        int curWeapon = hand.GetCurWeaponIdx();
        weapons[curWeapon].color = curWeaponColor;
    }
}
