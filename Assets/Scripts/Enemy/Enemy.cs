
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Entity
{
    public EnemyData enemyData;
    public BehaviorDesigner.Runtime.BehaviorTree behaviorTree;
    public UnityEvent died;
    public int health = 1;
    private GameObject _target;
    [SerializeField]
    private float dashCooldownTimer = 0;
    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player");

        if (_target == null)
            Debug.LogAssertion("Target is null");
        if (enemyData == null)
            Debug.LogAssertion("EnemyData is null");
        if (behaviorTree == null)
            Debug.LogAssertion("BehaviorTree is null");
        behaviorTree = GetComponent<BehaviorDesigner.Runtime.BehaviorTree>();
        health = enemyData.health;
        behaviorTree.SetVariableValue("moveSpeed", enemyData.moveSpeed);
        behaviorTree.SetVariableValue("attackRange", enemyData.attackRange);
        behaviorTree.SetVariableValue("attackWindupTime", enemyData.attackWindupTime);
        behaviorTree.SetVariableValue("targetTransform", _target.transform);
        behaviorTree.SetVariableValue("selfTransform", transform);
        behaviorTree.SetVariableValue("projectileData", enemyData.projectileData);
        switch (enemyData.enemyType)
        {
            case EnemyType.dasher:
                //behaviorTree.SetVariableValue("dashCurve", enemyData.dashCurve);
                behaviorTree.SetVariableValue("dashSpeed", enemyData.dashSpeed);
                behaviorTree.SetVariableValue("dashTime", enemyData.dashTime);
                behaviorTree.SetVariableValue("dashCooldown", enemyData.dashCooldown);
                behaviorTree.SetVariableValue("dashWindupTime", enemyData.dashWindupTime);
                break;
        }
    }
    private void Update()
    {
        //Cooldown timer for dash
        switch (enemyData.enemyType)
        {
            case EnemyType.dasher:
            dashCooldownTimer = (float)behaviorTree.GetVariable("dashCooldownTimer").GetValue();
                if (dashCooldownTimer > 0)
                {
                    dashCooldownTimer -= Time.deltaTime;
                    behaviorTree.SetVariableValue("dashCooldownTimer", dashCooldownTimer);
                }
                break;
        }

    }

    [Button("Kill")]
    public void Kill()
    {
        died.Invoke();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var proj = col.gameObject.GetComponent<Projectile>();
        if (proj != null && proj.args.DamageInfo.ownerType == ProjectileOwnerType.player)
        {
            Destroy(col.gameObject);
            Hurt(1);
        }
    }

    public void Hurt(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Kill();
        }
    }
}
