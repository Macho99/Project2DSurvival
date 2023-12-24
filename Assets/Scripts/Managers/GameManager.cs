using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static DataManager dataManager;
    public static GameManager Instance { get { return instance; } }
    public static DataManager Data { get { return dataManager; } }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        InitManagers();
    }

    private void InitManagers()
    {
        GameObject dataObj = new GameObject("DataManager");
        dataObj.transform.parent = transform;
        dataManager = dataObj.AddComponent<DataManager>();
    }
}