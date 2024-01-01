using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text= GetComponent<TextMeshProUGUI>();
    }
    public void UIUpdate()
    {
        text.text = ObjPool.Instance.monsterCount.ToString();
    }
}
