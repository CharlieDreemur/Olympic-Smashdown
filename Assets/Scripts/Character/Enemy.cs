using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyData enemyData;
    public GameObject target;
    public BehaviorDesigner.Runtime.BehaviorTree behaviorTree;
    public int health;
    private void Awake() {
        behaviorTree = GetComponent<BehaviorDesigner.Runtime.BehaviorTree>();
        health = enemyData.health;
        behaviorTree.SetVariableValue("moveSpeed", enemyData.moveSpeed);
        behaviorTree.SetVariableValue("attackRange", enemyData.attackRange);
        behaviorTree.SetVariableValue("attackWindupTime", enemyData.attackWindupTime);
        behaviorTree.SetVariableValue("targetTransform", target.transform);
        behaviorTree.SetVariableValue("selfTransform", transform);
        behaviorTree.SetVariableValue("projectileData", enemyData.projectileData);
    }

}
