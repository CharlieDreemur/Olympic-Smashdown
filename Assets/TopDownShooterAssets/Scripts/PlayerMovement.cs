using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveV;
    private float dashMoveH, dashMoveV;
    [SerializeField] private float moveSpeed;

    public GameObject shadowPrefab;

    [Header("Dashing")]
    public float dashSpeedMultiplier;
    public float dashDuration;

    private float startDashTime;

    [Header("Internal States")]
    public bool isDashing;

    private int frameCount;

    [Header("Player Stats")]
    public float hit_back_factor;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        frameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moveH = Input.GetAxis("Horizontal") * moveSpeed;
        moveV = Input.GetAxis("Vertical") * moveSpeed;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            ProcessDash();
        }
    }

    private void ProcessDash()
    {
        isDashing = true;
        startDashTime = Time.time;
        dashMoveH = rb.velocity.x * dashSpeedMultiplier;
        dashMoveV = rb.velocity.y * dashSpeedMultiplier;
        // ShadowPool.instance.SetPrefabAndTransform(shadowPrefab, transform);
    }

    private void FixedUpdate()
    {
        frameCount++;
        if (!isDashing)
        {
            rb.velocity = new Vector2(moveH, moveV);
        }
        else
        {
            rb.velocity = new Vector2(dashMoveH, dashMoveV);

            // if (frameCount % ShadowPool.instance.framePerShadow == 0) ShadowPool.instance.GetFromPool();

            if (Time.time >= startDashTime + dashDuration)
            {
                isDashing = false;
            }
        }
            

        Flip();
    }

    private void Flip()
    {
        if (transform.position.x < Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
        {
            transform.eulerAngles = Vector3.zero;
        } else if (transform.position.x > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
