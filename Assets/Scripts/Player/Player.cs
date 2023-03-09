using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Player : Entity
{
    public PlayerData data;

    // weird dependency but it is probably fine
    public TransitionBlackout curtain;


    // some external manager will need to set the health on game start
    // and make it remember its health across levels
    public PlayerStats playerStats;
    [FoldoutGroup("Events")]
    public UnityEvent onStart;
    [FoldoutGroup("Events")]
    public UnityEvent onUpdate;
    [FoldoutGroup("Events")]
    public UnityEvent onHurt;
    [FoldoutGroup("Events")]
    public UnityEvent<int, int> onHealthChange;
    [FoldoutGroup("Events")]
    public UnityEvent onDash;
    [FoldoutGroup("Events")]
    public UnityEvent onReflect;
    private void Awake()
    {
        // for single scene setup, let's just set it to default value on start
        // note that this will need to be changed as we add more levels
        // probably some don't destroy on load setup
        playerStats.Init(data);
        onHealthChange.Invoke(playerStats.currentHealth, playerStats.maxHealth);
    }
    private void Start()
    {
        playerStats.currentHealth = playerStats.maxHealth;
        onStart.Invoke();
    }
    private void Update()
    {
        onUpdate.Invoke();
    }

    public void Hurt(int damage)
    {
        onHurt.Invoke();
        onHealthChange.Invoke(playerStats.currentHealth, playerStats.maxHealth);
        playerStats.currentHealth -= damage;
        if (playerStats.currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        StartCoroutine(curtain.LoadAsyncSceneWithFadeOut("GameOverScene"));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Projectile>() != null)
        {
            Destroy(col.gameObject);
            Hurt(1);
        }
    }
}
