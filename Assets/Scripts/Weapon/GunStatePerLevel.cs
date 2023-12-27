using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GunStatePerLevel : WeaponStatPerLevel
{
    public float accuracy = 0f;
    public float bulletSpeed = 0f;

    public float numOfBulletPerShot = 0f;
    public float shotAngle = 0f;
}