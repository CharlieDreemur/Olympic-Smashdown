
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour, IEnemy
{
    [Header("Parameters")]
    public float moveSpeed;
    public Transform target;


    public float maxHP;
    protected float curHP;
    protected Rigidbody2D rb;
    public float melee_damage;

    [Header("Hurt Effects")]
    protected SpriteRenderer sr;
    public float hurtDuration;
    protected float hurtCounter;

    [Header("Gam Objects")]
    public GameObject explosionEffect;
    public GameObject health_bar;

    // internal vars
    protected float hitBackFactor;
    protected EnemyHealthBar enemy_health_bar;
    protected CombatManager combat_manager; 


    public virtual void Start()
    {
        curHP = maxHP;
        // target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        enemy_health_bar = health_bar.GetComponent<EnemyHealthBar>();
        combat_manager = FindObjectOfType<CombatManager>();
        if (combat_manager == null) Debug.LogError("combat manager can not be found.");
        // Debug.Log(combat_manager.kill_count);
    }


    public virtual void Update()
    {
        if (hurtCounter <= 0)
        {
            sr.material.SetFloat("_FlashAmount", 0);
        } else {
            sr.material.SetFloat("_FlashAmount", hurtCounter / hurtDuration);
            hurtCounter -= Time.deltaTime;
        }
    }

    protected void FollowTarget(Transform target)
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float _amount, float _hit_back_factor, Transform instigator)
    {
        hitBackFactor = _hit_back_factor;
        curHP -= _amount;
        HurtFlash();
        HitBack(instigator);
        enemy_health_bar.SetHealth(maxHP, curHP);

        if (curHP <= 0)
        {
            combat_manager.HandleEnemyDeath(gameObject);
            Destroy(gameObject);
            // Instantiate(explosionEffect, transform.position, transform.rotation);
        }
    }

    protected void HurtFlash()
    {
        sr.material.SetFloat("_FlashAmount", 1);
        hurtCounter = hurtDuration;
    }

    protected void HitBack(Transform _instigator)
    {
        Vector2 diff = (_instigator.position - transform.position) * hitBackFactor * -1;
        transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y); 
    }

    protected void HurtPlayer(GameObject _player, float _amount) {
        _player.GetComponent<IPlayer>().DamagePlayer(_amount);
    }
}
