using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public PlayerData data;

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
    
    public void Hurt(int damage)
    {
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
