using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    int minutes;
    int seconds;

    private void Awake()
    {
        minutes = 0;
        seconds = 0;
    }
    public void TimeChange()
    {
        seconds++;
        if(seconds > 60)
        {
            seconds = 0;
            minutes++;
        }
        text.text = $"{minutes} : {seconds}";
    }
}
