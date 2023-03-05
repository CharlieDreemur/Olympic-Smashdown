using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Vector2 direction = Vector2.zero;
    [SerializeField] ProjectileInfo projectileInfo;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        InitProjectile();
    }

    private void InitProjectile()
    {
        rb.velocity = direction.normalized * projectileInfo.speedStart;
        transform.localScale = new Vector3(projectileInfo.size, projectileInfo.size, 1f);
    }

    // Apply ProjectileInfo custom behavior in FixedUpdate, OnCollisonEnter2D, etc.

    public static Projectile InstantiateProjectile(Vector2 direction, Projectile projectilePrefab)
    {
        Projectile projectile = Instantiate(projectilePrefab);
        projectile.direction = direction;
        return projectile;
    }
}

[Serializable]
public struct ProjectileInfo
{
    public float speedStart;
    public float speedIncreasePerSecond;
    public float speedIncreaseOnCollision;
    public float size;
    public float damageMultiplier;
    public float despawnTime;
    public bool isHoming;

    // Add more custom behavior
}
