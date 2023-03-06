using System;
using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Enemy : Entity
{
    public EnemyData enemyData;
    [Required]
    public GameObject target;
    public BehaviorDesigner.Runtime.BehaviorTree behaviorTree;
    public int health;
    private void Awake() {
        if(target == null)
            Debug.LogAssertion("Target is null");
        if(enemyData == null)
            Debug.LogAssertion("EnemyData is null");
        if(behaviorTree == null)
            Debug.LogAssertion("BehaviorTree is null");
        behaviorTree = GetComponent<BehaviorDesigner.Runtime.BehaviorTree>();
        health = enemyData.health;
        behaviorTree.SetVariableValue("moveSpeed", enemyData.moveSpeed);
        behaviorTree.SetVariableValue("attackRange", enemyData.attackRange);
        behaviorTree.SetVariableValue("attackWindupTime", enemyData.attackWindupTime);
        behaviorTree.SetVariableValue("targetTransform", target.transform);
        behaviorTree.SetVariableValue("selfTransform", transform);
        behaviorTree.SetVariableValue("projectileData", enemyData.projectileData);
        switch(enemyData.enemyType){
            case EnemyType.dasher:
                behaviorTree.SetVariableValue("dashCurve", enemyData.dashCurve);
                behaviorTree.SetVariableValue("dashSpeed", enemyData.dashSpeed);
                behaviorTree.SetVariableValue("dashTime", enemyData.dashTime);
                break;
        }
        
    }

}
