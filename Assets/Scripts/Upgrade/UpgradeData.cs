using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public enum UpgradeType
{
    one,
    two,
    three
}

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Data/UpgradeData", order = 0)]
public class UpgradeData : ScriptableObject
{
    public Sprite icon;

    public new string name;

    [TextArea(2, 2)]
    public string description;
    public string funFact; // one fun sentence illutrate this powerup //"wtf is this?" BY Charlie

    public StatsBonus statsBonus;
    [System.Serializable]
    public struct StatsBonus
    {
        [FoldoutGroup("Player")]
        public int healthBonus;
        [FoldoutGroup("Player")]
        public float moveSpeedBonus;
        [FoldoutGroup("Player")]
        public float playerSizeBonus;
        [FoldoutGroup("Reflect")]
        public int reflectDamageBonus;
        [FoldoutGroup("Reflect")]
        public float reflectMoveSpeedBonus;
        [FoldoutGroup("Racket")]
        public int racketDamageBonus;
        [FoldoutGroup("Racket")]
        public float racketAttackSpeedBonus;
        [FoldoutGroup("Racket")]
        public float racketSizeBonus;
        public void ApplyBonus(Player player){
            player.health += healthBonus;
            player.moveSpeed += moveSpeedBonus;
            player.racketSize += racketSizeBonus;
            player.racketDamage += racketDamageBonus;
            player.racketAttackSpeed += racketAttackSpeedBonus;
        }

    }



}
