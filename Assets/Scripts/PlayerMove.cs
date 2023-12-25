using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;

    private SpriteRenderer spRenderer;
    private Hand hand;
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        spRenderer = GetComponentInChildren<SpriteRenderer>();
        rb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hand = GetComponentInChildren<Hand>();
    }

    private void OnMove(InputValue value)
    {
        Vector2 vec = value.Get<Vector2>();
        rb.velocity = vec * moveSpeed;
    }

    private void FixedUpdate()
    {
        anim.SetFloat("Speed", rb.velocity.sqrMagnitude);

        if (rb.velocity.x < 0f)
        {
            spRenderer.flipX = true;
            hand.FlipX(true);
        }
        else if (rb.velocity.x > 0f)
        {
            spRenderer.flipX = false;
            hand.FlipX(false);
        }
    }
}