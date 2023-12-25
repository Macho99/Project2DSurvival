using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneMaster : MonoBehaviour
{
    private static PlaySceneMaster instance;
    public static PlaySceneMaster Instance { get { return instance; } }
    public Player Player { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}