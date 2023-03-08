using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[EnumToggleButtons]
public enum EnemyType{
    normal,
    dasher,
    boss
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData", order = 0)]
public class EnemyData : ScriptableObject{
    public int health = 5;
    public EnemyType enemyType = EnemyType.normal;
    public float moveSpeed = 5;
    [ShowIf("enemyType", EnemyType.dasher)]
    public AnimationCurve dashCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [ShowIf("enemyType", EnemyType.dasher)]
    public float dashSpeed = 15; 
    [ShowIf("enemyType", EnemyType.dasher)]
    public float dashTime = 2;
    [ShowIf("enemyType", EnemyType.dasher)]
    public float dashWindupTime = 0.5f;
    [InfoBox("Dash cooldown means that when it dash once, it will only dash next time after 5 seconds")]
    [ShowIf("enemyType", EnemyType.dasher)]
    public float dashCooldown = 5; 
    public float attackRange;
    public float attackWindupTime;
    public ProjectileData projectileData;

}
