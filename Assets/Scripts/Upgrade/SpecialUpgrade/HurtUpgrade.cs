using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "HurtUpgrade", menuName = "Upgrade/HurtUpgrade", order = 0)]
public class HurtUpgrade : SpecialUpgrade, IUpgrade
{
    public PlayerStats Stats;
    public override void OnHurt()
    {
        Player.Instance.playerStats.Add(Stats);
    }

}