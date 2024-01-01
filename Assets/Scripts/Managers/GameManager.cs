using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static DataManager dataManager;
    //private static SkillManager skillManager;
    public static GameManager Instance { get { return instance; } }
    public static DataManager Data { get { return dataManager; } }
    //public static SkillManager Skill { get { return skillManager; } }

    [SerializeField] DataManager dataManagerPrefab;
    [SerializeField] SkillManager skillManagerPrefab;

    public UnityEvent onFpsChange;
    public int fps;
    private int frameCnt;
    private float timeCnt;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        dataManager = GetComponentInChildren<DataManager>();
        //skillManager= GetComponentInChildren<SkillManager>();
        DontDestroyOnLoad(gameObject);
        InitManagers();

        frameCnt = 0;
        timeCnt = 0f;
    }

    private void Update()
    {
        frameCnt++;
        timeCnt += Time.deltaTime;
        if(timeCnt > 1f)
        {
            timeCnt -= 1f;
            fps = frameCnt;
            frameCnt = 0;
            onFpsChange?.Invoke();
        }
    }

    private void InitManagers()
    {
        //dataManager = Instantiate(dataManagerPrefab);
        //dataManager.transform.parent = transform;

        //skillManager = Instantiate(skillManagerPrefab);
        //skillManager.transform.parent = transform;
    }
}