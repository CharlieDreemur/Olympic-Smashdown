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
    private int currentHealth;
    [MinValue(0)] [MaxValue("@maxHealth")] [ShowInInspector]
    public int CurrentHealth{
        get => currentHealth;
        set{
            currentHealth = value;
            if(currentHealth > maxHealth){
                currentHealth = maxHealth;
            }
            if(currentHealth < 0){
                currentHealth = 0;
            }
            Player.Instance.onHealthChange.Invoke(CurrentHealth, MaxHealth);
        }
    }

    [FoldoutGroup("Player")]
    private int maxHealth;
    [MinValue(1)] [ShowInInspector]
    public int MaxHealth{
        get => maxHealth;
        set{
            maxHealth = value;
            if(currentHealth > maxHealth){
                currentHealth = maxHealth;
            }
            Player.Instance.onHealthChange.Invoke(CurrentHealth, MaxHealth);
        }
    }
    [FoldoutGroup("Player")]
    public float moveSpeed;
    [FoldoutGroup("Player")]
    public float moveSpeedMultiplier;
    [FoldoutGroup("Player")]
    public float playerSizeMultiplier;
    [FoldoutGroup("Reflect")]
    public int reflectDamage;
    [FoldoutGroup("Reflect")]
    public float reflectDamageMultiplier;
    [FoldoutGroup("Reflect")]
    public float reflectMoveSpeed;
    [FoldoutGroup("Reflect")]
    public float reflectMoveSpeedMultiplier;
    [FoldoutGroup("Racket")]
    public int racketDamage;
    [FoldoutGroup("Racket")]
    public int racketDamageMultipler;
    [FoldoutGroup("Racket")]
    public float reflectScaleMultiplier;
    [FoldoutGroup("Racket")]
    public float racketSwingCooldown;
    [FoldoutGroup("Racket")]
    public float racketSizeMultiper;
    public void Init(PlayerData data)
    {
        currentHealth = data.stats.currentHealth;
        maxHealth = data.stats.maxHealth;
        moveSpeed = data.stats.moveSpeed;
        moveSpeedMultiplier = data.stats.moveSpeedMultiplier;
        playerSizeMultiplier = data.stats.racketSizeMultiper;
        reflectDamage = data.stats.reflectDamage;
        reflectDamageMultiplier = data.stats.racketDamageMultipler;
        reflectMoveSpeed = data.stats.reflectMoveSpeed;
        reflectMoveSpeedMultiplier = data.stats.reflectMoveSpeedMultiplier;
        reflectScaleMultiplier = data.stats.reflectScaleMultiplier;
        racketDamage = data.stats.racketDamage;
        racketDamageMultipler = data.stats.racketDamageMultipler;
        racketSwingCooldown = data.stats.racketSwingCooldown;
        racketSizeMultiper = data.stats.racketSizeMultiper;
    }
    public void Add(PlayerStats stats)
    {
        currentHealth += stats.currentHealth;
        maxHealth += stats.maxHealth;
        moveSpeed += stats.moveSpeed;
        moveSpeedMultiplier += stats.moveSpeedMultiplier;
        playerSizeMultiplier += stats.playerSizeMultiplier; 
        reflectDamage += stats.reflectDamage;
        reflectDamageMultiplier += stats.reflectDamageMultiplier;
        reflectMoveSpeed += stats.reflectMoveSpeed;
        reflectMoveSpeedMultiplier += stats.reflectMoveSpeedMultiplier;
        reflectScaleMultiplier += stats.reflectScaleMultiplier;
        racketDamage += stats.racketDamage;
        racketDamageMultipler += stats.racketDamageMultipler;
        racketSwingCooldown += stats.racketSwingCooldown;
        racketSizeMultiper += stats.racketSizeMultiper;

    }
    public void Minus(PlayerStats stats)
    {   
        currentHealth -= stats.currentHealth;
        maxHealth -= stats.maxHealth;
        moveSpeed -= stats.moveSpeed;
        moveSpeedMultiplier -= stats.moveSpeedMultiplier;
        playerSizeMultiplier -= stats.playerSizeMultiplier; 
        reflectDamage -= stats.reflectDamage;
        reflectDamageMultiplier -= stats.reflectDamageMultiplier;
        reflectMoveSpeed -= stats.reflectMoveSpeed;
        reflectMoveSpeedMultiplier -= stats.reflectMoveSpeedMultiplier;
        reflectScaleMultiplier -= stats.reflectScaleMultiplier;
        racketDamage -= stats.racketDamage;
        racketDamageMultipler -= stats.racketDamageMultipler;
        racketSwingCooldown -= stats.racketSwingCooldown;
        racketSizeMultiper -= stats.racketSizeMultiper;
    }

}