using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData", order = 0)]
public class EnemyData : ScriptableObject{
    public int health = 5;
    public float moveSpeed = 5;
    public float attackRange;
    public float attackWindupTime;
    public ProjectileData projectileData;

}
