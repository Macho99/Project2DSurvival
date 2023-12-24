using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;

    private Rigidbody2D rb;
    private SpriteRenderer spRenderer;
    private Animator anim;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void OnMove(InputValue value)
    {
        Vector2 vec = value.Get<Vector2>();
        rb.velocity = vec * moveSpeed;
    }

    private void FixedUpdate()
    {
        anim.SetFloat("Speed", rb.velocity.sqrMagnitude);

        if(rb.velocity.x < 0)
        {
            spRenderer.flipX = true;
        }
        else if(rb.velocity.x > 0)
        {
            spRenderer.flipX = false;
        }
    }
}