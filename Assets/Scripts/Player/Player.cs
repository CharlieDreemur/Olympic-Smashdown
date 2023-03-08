using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Entity
{
    public PlayerData data;
    public UnityEvent onStart;
    public UnityEvent onUpdate;
    public UnityEvent onHurt;
    public UnityEvent onDash;
    public UnityEvent onReflect;


    // weird dependency but it is probably fine
    public TransitionBlackout curtain;
    
    
    // some external manager will need to set the health on game start
    // and make it remember its health across levels
    public int health;

    private void Awake()
    {   
        // for single scene setup, let's just set it to default value on start
        // note that this will need to be changed as we add more levels
        // probably some don't destroy on load setup
        health = data.health;
    }
    private void Start() {
        onStart.Invoke();
    }
    private void Update() {
        onUpdate.Invoke();
    }

    public void Hurt(int damage)
    {
        onHurt.Invoke();
        health -= damage;
        if (health <= 0)
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
