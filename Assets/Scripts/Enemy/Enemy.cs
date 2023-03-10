
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
    private Animator _animator;
    [SerializeField]
    private float dashCooldownTimer = 0;
    [SerializeField]
    private float attackCooldownTimer = 0;
    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
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
        behaviorTree.SetVariableValue("attackPrepareTime", enemyData.attackPrepareTime);
        behaviorTree.SetVariableValue("attackWindupTime", enemyData.attackWindupTime);
        behaviorTree.SetVariableValue("attackCooldown", enemyData.attackCooldown);
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
        //Cooldown timer for attack
        attackCooldownTimer = (float)behaviorTree.GetVariable("attackCooldownTimer").GetValue();
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
            behaviorTree.SetVariableValue("attackCooldownTimer", attackCooldownTimer);
        }
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
        behaviorTree.enabled = false;
        _animator.Play("Die");
        //Destroy(gameObject) after die animation is done
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        StartCoroutine(Death(stateInfo.length));
    }

    private void OnTriggerEnter2D(Collider2D col)
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
        string jsonValue = JsonUtility.ToJson(new CreateDamageTextEventArgs(transform.position, damage, DamageType.Normal));
        EventManager.Invoke("CreateDamageText", jsonValue);
        health -= damage;
        if (health <= 0)
        {
            Kill();
        }
    }

    IEnumerator Death(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
