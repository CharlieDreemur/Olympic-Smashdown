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
    public int currentHealth;
    [FoldoutGroup("Player")]
    public int maxHealth;
    [FoldoutGroup("Player")]
    public float moveSpeed;
    [FoldoutGroup("Player")]
    public float moveSpeedMultipler;
    [FoldoutGroup("Player")]
    public float playerSizeMultipler;
    [FoldoutGroup("Reflect")]
    public int reflectDamageMultipler;
    [FoldoutGroup("Reflect")]
    public float reflectMoveSpeedMultipler;
    [FoldoutGroup("Racket")]
    public int racketDamageMultipler;
    [FoldoutGroup("Racket")]
    public float racketSwingCooldown;
    [FoldoutGroup("Racket")]
    public float racketSizeMultiper;
    public void Init(PlayerData data)
    {
        currentHealth = data.stats.currentHealth;
        maxHealth = data.stats.maxHealth;
        moveSpeed = data.stats.moveSpeed;
        playerSizeMultipler = data.stats.racketSizeMultiper;
        reflectDamageMultipler = data.stats.racketDamageMultipler;
        reflectMoveSpeedMultipler = data.stats.reflectMoveSpeedMultipler;
        racketDamageMultipler = data.stats.racketDamageMultipler;
        racketSwingCooldown = data.stats.racketSwingCooldown;
        racketSizeMultiper = data.stats.racketSizeMultiper;
    }
    public void Add(PlayerStats stats)
    {
        currentHealth += stats.currentHealth;
        maxHealth += stats.maxHealth;
        moveSpeed += stats.moveSpeed;
        playerSizeMultipler += stats.playerSizeMultipler; 
        reflectDamageMultipler += stats.reflectDamageMultipler;
        reflectMoveSpeedMultipler += stats.reflectMoveSpeedMultipler;
        racketDamageMultipler += stats.racketDamageMultipler;
        racketSwingCooldown += stats.racketSwingCooldown;
        racketSizeMultiper += stats.racketSizeMultiper;

    }
    public void Minus(PlayerStats stats)
    {   
        currentHealth -= stats.currentHealth;
        maxHealth -= stats.maxHealth;
        moveSpeed -= stats.moveSpeed;
        playerSizeMultipler -= stats.playerSizeMultipler; 
        reflectDamageMultipler -= stats.reflectDamageMultipler;
        reflectMoveSpeedMultipler -= stats.reflectMoveSpeedMultipler;
        racketDamageMultipler -= stats.racketDamageMultipler;
        racketSwingCooldown -= stats.racketSwingCooldown;
        racketSizeMultiper -= stats.racketSizeMultiper;
    }

}