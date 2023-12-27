using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillManager : MonoBehaviour
{
    [SerializeField] Weapon[] weaponPrefabs;
    [SerializeField] SelectSkill selectSkillUI;

    private static SkillManager instance;
    public static SkillManager Instance { get { return instance; } }

    //Player player;
    //Hand hand;

    //private void Start()
    //{
    //    player = PlaySceneMaster.Instance.Player;
    //    hand = player.GetHand();
    //}

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void SelectSkill()
    {
        if (PlaySceneMaster.Instance.Player.Level <= 1) return;

        Time.timeScale = 0f;

        List<int> list = new List<int>();
        while (true)
        {
            int idx = Random.Range(0, weaponPrefabs.Length);
            if (!list.Contains(idx))
            {
                list.Add(idx);
            }

            if(list.Count >= 3)
            {
                break;
            }
        }

        Weapon[] candidateWeapons = new Weapon[3];
        bool[] isNews = new bool[3];

        for(int i = 0; i < list.Count; i++)
        {
            if (PlaySceneMaster.Instance.Player.GetHand().HaveWeapon(weaponPrefabs[list[i]].GetName()))
            {
                isNews[i] = false;
            }
            else
            {
                isNews[i] = true;
            }
            candidateWeapons[i] = weaponPrefabs[list[i]];
        }
        selectSkillUI.Init(candidateWeapons, isNews);
    }

    public void OnSkillSelect(string name)
    {
        Hand hand = PlaySceneMaster.Instance.Player.GetHand();
        if (hand.HaveWeapon(name))
        {
            hand.WeaponLevelUp(name);
        }
        else
        {
            Weapon weapon = Instantiate(FindWeaponPrefab(name));
            hand.ReplaceWeapon(weapon);
        }
        Time.timeScale = 1f;
    }

    private Weapon FindWeaponPrefab(string name)
    {
        foreach(var prefab in weaponPrefabs)
        {
            if (prefab.GetName().Equals(name))
            {
                return prefab;
            }
        }
        return null;
    }
}