using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    [SerializeField]
    private bool isDash = false;
    [SerializeField]
    private bool canDash = true;
    private void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); ;
    }

    private void Update()
    {

        if (isDash) return;

        // Get the horizontal and vertical input axes
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        if (horizontalInput > 0)
            spriteRenderer.flipX = false;
        else if (horizontalInput < 0)
            spriteRenderer.flipX = true;

        // Calculate the movement vector
        movement = new Vector2(horizontalInput, verticalInput).normalized * player.playerStats.moveSpeed * player.playerStats.moveSpeedMultiplier;
        if (canDash && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Dash(movement));
        }
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
    public IEnumerator Dash(Vector2 dashDirection)
    {
        isDash = true;
        canDash = false;
        // Start the dash
        Vector2 startPosition = transform.position;
        float dashDuration = 0.5f;
        float dashSpeed = Player.Instance.playerStats.moveSpeed * 0.2f;
        float dashCooldownTime = 2f;
        float flashDuration = 0.1f;
        Color newColor = Color.white;
        Sequence flashSequence = DOTween.Sequence();
        flashSequence.Append(spriteRenderer.DOColor(new Color(newColor.r, newColor.g, newColor.b, 0f), flashDuration / 2))
                     .Append(spriteRenderer.DOColor(newColor, flashDuration / 2))
                     .SetLoops(1)
                     .OnComplete(() => spriteRenderer.color = newColor)
                     .Play();
        transform.DOMove(transform.position + new Vector3(dashDirection.x, dashDirection.y, 0) * dashSpeed, dashDuration);
        isDash = false;
        // End the dash and implement cooldown
        yield return new WaitForSeconds(dashCooldownTime);
        canDash = true;
    }
}