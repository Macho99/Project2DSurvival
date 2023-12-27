using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjPoolType
{
    Bullet,
    Monster,
    Coin,

    Size
}

[Serializable]
public class PoolElement {
    public ObjPoolType type;
    public GameObject elementPrefab;
    public int initPoolSize;
    [HideInInspector] public Transform poolFolder;
    [HideInInspector] public Transform runtimeFolder;
}

public class ObjPool : MonoBehaviour
{
    private static ObjPool instance;
    public static ObjPool Instance { get { return instance; } }

    //�ν����Ϳ��� ObjPoolType ������� ���� ��!!
    [SerializeField] private PoolElement[] poolElements;

    private Stack<GameObject>[] pools;
    private Transform runtimeObjFolder;
    private Transform poolObjFolder;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        Init();
    }

    private void Init()
    {
        runtimeObjFolder = new GameObject("RuntimeObject").transform;
        runtimeObjFolder.parent = transform;

        poolObjFolder = new GameObject("PoolObject").transform;
        poolObjFolder.parent = transform;

        pools = new Stack<GameObject>[(int)ObjPoolType.Size];

        for(int elemIdx=0 ; elemIdx < poolElements.Length; elemIdx++)
        {
            PoolElement elem = poolElements[elemIdx];
            if(pools[(int) elem.type] != null)
            {
                Debug.LogError("ObjPool: type�� �ߺ��� poolElement�� �ֽ��ϴ�!");
                return;
            }

            pools[(int)elem.type] = new Stack<GameObject>();
            Transform elemPoolFolder = new GameObject(elem.type.ToString()).transform;
            elem.poolFolder = elemPoolFolder;
            elemPoolFolder.transform.parent = poolObjFolder;

            Transform elemRuntimeFolder = new GameObject(elem.type.ToString()).transform;
            elem.runtimeFolder = elemRuntimeFolder;
            elemRuntimeFolder.transform.parent = runtimeObjFolder;

            for(int i = 0; i < elem.initPoolSize; i++)
            {
                GameObject instance = Instantiate(elem.elementPrefab, elemPoolFolder.transform);
                pools[(int)elem.type].Push(instance);
                instance.SetActive(false);
            }
        }
    }

    public GameObject AllocateObj(ObjPoolType type)
    {
        GameObject obj;
        if (pools[(int) type].Count == 0)
        {
            obj = Instantiate(poolElements[(int)type].elementPrefab);
        }
        else
        {
            obj = pools[(int)type].Pop();
            obj.SetActive(true);
        }
        obj.transform.parent = poolElements[(int) type].runtimeFolder;

        return obj;
    }

    public void ReturnObj(ObjPoolType type, GameObject obj)
    {
        pools[(int) type].Push(obj);
        obj.transform.parent = poolElements[(int)type].poolFolder;
        obj.SetActive(false);
    }

    private void OnDestroy()
    {
        Release();
    }

    private void Release()
    {

    }
}