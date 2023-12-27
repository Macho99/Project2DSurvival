using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    [SerializeField] private MeleeProjectile projectilePrefab;

    [SerializeField] private float scale = 1f;

    [SerializeField] private WeaponStatPerLevel statPerLevel;


    private MeleeProjectile projectile;
    protected override void Awake()
    {
        base.Awake();
        HandPos = new Vector2(-0.18f, -0.29f);
        projectile = Instantiate(projectilePrefab);
        projectile.transform.parent = transform;
        projectile.gameObject.SetActive(false);
    }

    public override void Effect()
    {
        projectile.transform.localPosition = Vector2.right;
        projectile.gameObject.SetActive(true);
    }

    public override void LevelUp()
    {
        WeaponLevelUp(statPerLevel);
    }
}
