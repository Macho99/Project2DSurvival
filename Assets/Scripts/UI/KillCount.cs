using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    
    private Player player;

    private void Start()
    {
        player = PlaySceneMaster.Instance.Player;
    }

    public void UIUpdate()
    {
        text.text = player.KillCnt.ToString();
    }
}
