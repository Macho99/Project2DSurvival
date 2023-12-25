using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerMove playerMove;
    private PlayerAim playerAim;
    private Hand hand;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        playerAim = GetComponent<PlayerAim>();
        hand = GetComponentInChildren<Hand>();
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
}
