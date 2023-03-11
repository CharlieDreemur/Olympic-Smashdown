using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "LowHealthUpgrade", menuName = "Upgrade/LowHealthUpgrade", order = 0)]
public class LowHealthUpgrade : SpecialUpgrade, IUpgrade
{
    public PlayerStats Stats;
    [InfoBox("When player's health is lower than healthPercentage, add playerStats")]
    public float healthPercentage;
    private bool isAdd = false;
    public override void OnUpdate()
    {
            if (Player.Instance.playerStats.CurrentHealth < Player.Instance.playerStats.MaxHealth * healthPercentage)
            {
                if (isAdd) return;
                Player.Instance.playerStats.Add(Stats);
                isAdd = true;
            }
            else{
                Player.Instance.playerStats.Minus(Stats);
                isAdd = false;
            }

    }

}