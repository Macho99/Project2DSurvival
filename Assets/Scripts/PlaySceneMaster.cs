using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneMaster : MonoBehaviour
{
    private static PlaySceneMaster instance;
    public static PlaySceneMaster Instance { get { return instance; } }
    public GameObject Player { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
}