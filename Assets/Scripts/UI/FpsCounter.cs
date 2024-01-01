using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        GameManager.Instance.onFpsChange.AddListener(UIUpdate);
    }
    private void OnDisable()
    {
        GameManager.Instance.onFpsChange.RemoveListener(UIUpdate);
    }
    public void UIUpdate()
    {
        text.text = GameManager.Instance.fps.ToString();
    }
}
