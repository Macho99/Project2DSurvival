using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    [SerializeField] GameObject gameoverObj;

    public void OnGameover()
    {
        gameObject.SetActive(true);
    }
}
