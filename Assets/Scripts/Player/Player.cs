using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] UnityEvent onExpChange;
    [SerializeField] UnityEvent onHpChange;
    [SerializeField] UnityEvent onLevelChange;
    [SerializeField] UnityEvent onKillMonster;

    private PlayerMove playerMove;
    private PlayerAim playerAim;
    private Hand hand;

    public int CurExp {  get; private set; }
    public int MaxExp {  get; private set; }
    public int Level {  get; private set; }
    public int CurHp {  get; private set; }
    public int MaxHp {  get; private set; }
    public int HealAmount {  get; private set; }
    public float HealDuration {  get; private set; }
    public int KillCnt {  get; private set; }

    [SerializeField] private int[] expTable;
    [SerializeField] private int maxHp = 100;
    [SerializeField] private int healAmount = 1;
    [SerializeField] private float healDuration = 5f;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        playerAim = GetComponent<PlayerAim>();
        hand = GetComponentInChildren<Hand>();

        MaxHp = maxHp;
        CurHp = MaxHp;
        HealAmount = healAmount;
        HealDuration = healDuration;
        Level = 1;
        MaxExp = expTable[Level];
        KillCnt = 0;
    }

    private void Start()
    {
        StartCoroutine(CoStart());
        StartCoroutine(CoHeal());
    }

    private IEnumerator CoHeal()
    {
        while (true)
        {
            yield return new WaitForSeconds(HealDuration);
            CurHp += HealAmount;
            if(CurHp > MaxHp)
                CurHp = MaxHp;

            onHpChange?.Invoke();
        }
    }

    private IEnumerator CoStart()
    {
        yield return null;
        onHpChange?.Invoke();
        onExpChange?.Invoke();
        onKillMonster?.Invoke();
        onLevelChange?.Invoke();
    }

    public Transform GetAimTransform()
    {
        return playerAim.GetAimTransform();
    }

    private void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            hand.Fire();
        }
    }

    public void AddExp(int value)
    {
        CurExp += value;
        CheckLevelUp();
        onExpChange?.Invoke();
    }

    private void CheckLevelUp()
    {
        if(CurExp >= MaxExp)
        {
            Level++;
            CurExp -= MaxExp;
            MaxExp = expTable[Level];

            onLevelChange?.Invoke();

            CheckLevelUp();
        }
    }

    public void TakeDamage(int damage)
    {
        CurHp -= damage;
        onHpChange?.Invoke();

        if( CurHp <= 0)
        {
            CurHp = 0;
            Die();
        }
    }

    public void KillMonster()
    {
        KillCnt++;
        onKillMonster?.Invoke();
    }

    private void Die()
    {

    }
}