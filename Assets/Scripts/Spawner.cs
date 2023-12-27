using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnDist = 20;
    [SerializeField] private float spawnDuration = 1f;
    [SerializeField] private float spawnDurationDiff = 0.002f;

    [SerializeField] UnityEvent onSecondPassed;

    private Transform playerTrans;

    private void Start()
    {
        playerTrans = PlaySceneMaster.Instance.Player.transform;
        _ = StartCoroutine(CoSpawn());
        _ = StartCoroutine(CoTimer());
    }

    private IEnumerator CoTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            spawnDuration -= spawnDurationDiff;
            if(spawnDuration < 0.1f)
            {
                spawnDuration = 0.1f;
            }
            onSecondPassed?.Invoke();
        }
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

        GameObject monster = ObjPool.Instance.AllocateObj(ObjPoolType.Monster);
        monster.transform.position = pos;
    }
}