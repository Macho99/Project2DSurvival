using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Hand : MonoBehaviour
{
    private Weapon weapon;

    private void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
    }

    public void Fire()
    {
        weapon.Use();
    }

    public void FlipX(bool isFlip)
    {
        if(weapon != null)
        {
            weapon.FlipX(isFlip);
        }
    }
}
