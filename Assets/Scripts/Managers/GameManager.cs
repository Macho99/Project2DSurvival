using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static DataManager dataManager;
    private static SkillManager skillManager;
    public static GameManager Instance { get { return instance; } }
    public static DataManager Data { get { return dataManager; } }
    public static SkillManager Skill { get { return skillManager; } }

    [SerializeField] DataManager dataManagerPrefab;
    [SerializeField] SkillManager skillManagerPrefab;

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
        GameObject dataObj = Instantiate(dataManagerPrefab).gameObject;
        dataObj.transform.parent = transform;

        GameObject skillObj = Instantiate(skillManagerPrefab).gameObject;
        skillObj.transform.parent = transform;
    }
}