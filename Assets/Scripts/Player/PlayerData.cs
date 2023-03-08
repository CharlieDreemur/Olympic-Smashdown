using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    [MinValue(0)]
    [GUIColor(0.2f, 0.2f, 1f)]
    public float moveSpeed = 5f;
    [MinValue(0)]
    [GUIColor(0, 1, 0)]
    public int health = 3;
    [MinValue(0.1)]
    public int racketSize = 1;
    [MinValue(0)]
    public int racketDamage = 1;
    [MinValue(0.1)]
    public int racketSpeed = 1;

}
[System.Serializable] [HideLabel]
    public struct PlayerStats
    {
        [FoldoutGroup("Player")]
        public int health;
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
        public void Init(PlayerData data){
            health = data.health;
            moveSpeed = data.moveSpeed;
            playerSize = data.racketSize;
            reflectDamage = data.racketDamage;
            reflectMoveSpeed = data.racketSpeed;
            racketDamage = data.racketDamage;
            racketAttackSpeed = data.racketSpeed;
            racketSize = data.racketSize;
        }
        public void Add(PlayerStats stats){
            stats.health += health;
            stats.moveSpeed += moveSpeed;
            stats.playerSize += playerSize;
            stats.reflectDamage += reflectDamage;
            stats.reflectMoveSpeed += reflectMoveSpeed;
            stats.racketDamage += racketDamage;
            stats.racketAttackSpeed += racketAttackSpeed;
            stats.racketSize += racketSize;
        }
        public void Minus(PlayerStats stats){
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