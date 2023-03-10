using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Reaper Scythe", menuName = "Upgrade/Reaper Scythe", order = 0)]
public class ReaperScythe: SpecialUpgrade, IUpgrade{
    [Min(1)]
    public int killCount = 1;
    public int damageAdd = 1;
    public override void OnKillEnemy()
    {
        //For every killCount number of enemies kills, add reflectDamage by damageAdd
        Debug.Log("Player.Instance.enemyKilled: " + Player.Instance.enemyKilled);
        if(Player.Instance.enemyKilled % killCount == 0){
            Player.Instance.playerStats.reflectDamage += damageAdd;
        }
    }

}