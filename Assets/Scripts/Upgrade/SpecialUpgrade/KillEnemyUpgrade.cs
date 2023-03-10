using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "KillEnemyUpgrade", menuName = "Upgrade/KillEnemyUpgrade", order = 0)]
public class KillEnemyUpgrade: SpecialUpgrade, IUpgrade{
    [Min(1)]
    public int killCount = 1;
    [InfoBox("For every killCount number of enemies kills, add playerStats")]
    public PlayerStats Stats;
    public override void OnKillEnemy()
    {
        //For every killCount number of enemies kills, add reflectDamage by damageAdd
        Debug.Log("Player.Instance.enemyKilled: " + Player.Instance.enemyKilled);
        if(Player.Instance.enemyKilled % killCount == 0){
            Player.Instance.playerStats.Add(Stats);
        }
    }

}