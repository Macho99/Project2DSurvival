using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Image mask;

    private Player player;

    private void Start()
    {
        player = PlaySceneMaster.Instance.Player;
    }

    public void UIUpdate()
    {
        mask.fillAmount = (float)player.CurHp / player.MaxHp;
    }
}
