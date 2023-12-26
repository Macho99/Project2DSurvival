using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Image mask;
    [SerializeField] private TextMeshProUGUI text;

    private Player player;

    private void Start()
    {
        player = PlaySceneMaster.Instance.Player;
    }

    public void UIUpdate()
    {
        text.text = $"{player.CurExp} / {player.MaxExp}";
        mask.fillAmount = (float) player.CurExp / player.MaxExp;
    }
}
