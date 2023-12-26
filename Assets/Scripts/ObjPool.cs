using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjPoolType
{
    Bullet,
    Monster,

    Size
}

[Serializable]
public struct PoolElement {
    public ObjPoolType type;
    public GameObject elementPrefab;
    public int initPoolSize;
}

public class ObjPool : MonoBehaviour
{
    private static ObjPool instance;
    public static ObjPool Instance { get { return instance; } }

    [SerializeField] private PoolElement[] poolElements;

    private Stack<GameObject>[] pools;

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
        pools = new Stack<GameObject>[(int)ObjPoolType.Size];

        foreach(PoolElement elem in poolElements)
        {
            if(pools[(int) elem.type] != null)
            {
                Debug.LogError("ObjPool: type이 중복된 poolElement가 있습니다!");
                return;
            }
            pools[(int)elem.type] = new Stack<GameObject>();
            GameObject folder = new GameObject(elem.type.ToString());
            folder.transform.parent = transform;

            for(int i = 0; i < elem.initPoolSize; i++)
            {
                GameObject instance = Instantiate(elem.elementPrefab, folder.transform);
                pools[(int)elem.type].Push(instance);
                instance.SetActive(false);
            }
        }
    }

    public GameObject AllocateObj(ObjPoolType type)
    {
        GameObject Obj = pools[(int)type].Pop();
        Obj.transform.parent = null;
        Obj.SetActive(true);

        return Obj;
    }

    private void OnDestroy()
    {
        Release();
    }

    private void Release()
    {

    }
}