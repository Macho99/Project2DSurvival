using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private Player player;

    private void Start()
    {
        player = PlaySceneMaster.Instance.Player;
    }

    public void UIUpdate()
    {
        text.text = $"Lv. {player.Level}";
    }
}
