using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    private Player player;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private float moveSpeed;


    private void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveSpeed = player.playerStats.moveSpeed;
    }

    private void Update()
    {
        // Get the horizontal and vertical input axes
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput > 0)
            spriteRenderer.flipX = false;
        else if (horizontalInput < 0)
            spriteRenderer.flipX = true;

        // Calculate the movement vector
        movement = new Vector2(horizontalInput, verticalInput).normalized * moveSpeed;
        if (horizontalInput != 0 || verticalInput != 0)
        {
            animator.SetBool("isMove", true);
            animator.SetFloat("speed", movement.x + movement.y);
        }
        else
        {
            animator.SetBool("isMove", false);
        }
    }

    private void FixedUpdate()
    {
        // Apply the movement vector to the character's Rigidbody2D component
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }
}