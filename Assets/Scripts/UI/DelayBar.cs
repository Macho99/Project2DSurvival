using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayBar : MonoBehaviour
{
    [SerializeField] private Image backGround;
    [SerializeField] private Image mask;

    private Player player;

    private void Start()
    {
        player = PlaySceneMaster.Instance.Player;
    }

    public void UIUpdate()
    {
        mask.fillAmount = player.DelayRatio;
        if(player.DelayRatio > 0.99)
        {
            backGround.gameObject.SetActive(false);
            mask.gameObject.SetActive(false);
        }
        else
        {
            backGround.gameObject.SetActive(true);
            mask.gameObject.SetActive(true);
        }
    }
}
