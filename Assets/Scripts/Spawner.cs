using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnDist = 20;
    [SerializeField] private float spawnDuration = 1f;
    //[SerializeField] private Monster monsterPrefab;

    private Transform playerTrans;

    private void Start()
    {
        playerTrans = PlaySceneMaster.Instance.Player.transform;
        StartCoroutine(CoSpawn());
    }

    private IEnumerator CoSpawn()
    {
        while(true)
        {
            SpawnMonster();
            yield return new WaitForSeconds(spawnDuration);
        }

    }
    private void SpawnMonster()
    {
        float angle = Random.Range(0, 360);
        Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        pos *= spawnDist;
        pos += new Vector2(playerTrans.position.x, playerTrans.position.y);

        //Instantiate(monsterPrefab, pos, Quaternion.identity);
        GameObject monster = ObjPool.Instance.AllocateObj(ObjPoolType.Monster);
        monster.transform.position = pos;
    }
}