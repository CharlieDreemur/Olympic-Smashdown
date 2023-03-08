using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public PlayerStats stats;
}
[System.Serializable]
[HideLabel]
public struct PlayerStats
{
    [FoldoutGroup("Player")]
    public int health;
    [FoldoutGroup("Player")]
    public int maxHealth;
    [FoldoutGroup("Player")]
    public float moveSpeed;
    [FoldoutGroup("Player")]
    public float playerSize;
    [FoldoutGroup("Reflect")]
    public int reflectDamage;
    [FoldoutGroup("Reflect")]
    public float reflectMoveSpeed;
    [FoldoutGroup("Racket")]
    public int racketDamage;
    [FoldoutGroup("Racket")]
    public float racketAttackSpeed;
    [FoldoutGroup("Racket")]
    public float racketSize;
    public void Init(PlayerData data)
    {
        health = data.stats.health;
        moveSpeed = data.stats.moveSpeed;
        playerSize = data.stats.racketSize;
        reflectDamage = data.stats.racketDamage;
        reflectMoveSpeed = data.stats.reflectMoveSpeed;
        racketDamage = data.stats.racketDamage;
        racketAttackSpeed = data.stats.racketAttackSpeed;
        racketSize = data.stats.racketSize;
    }
    public void Add(PlayerStats stats)
    {
        stats.health += health;
        stats.moveSpeed += moveSpeed;
        stats.playerSize += playerSize;
        stats.reflectDamage += reflectDamage;
        stats.reflectMoveSpeed += reflectMoveSpeed;
        stats.racketDamage += racketDamage;
        stats.racketAttackSpeed += racketAttackSpeed;
        stats.racketSize += racketSize;
    }
    public void Minus(PlayerStats stats)
    {
        stats.health -= health;
        stats.moveSpeed -= moveSpeed;
        stats.playerSize -= playerSize;
        stats.reflectDamage -= reflectDamage;
        stats.reflectMoveSpeed -= reflectMoveSpeed;
        stats.racketDamage -= racketDamage;
        stats.racketAttackSpeed -= racketAttackSpeed;
        stats.racketSize -= racketSize;
    }

}