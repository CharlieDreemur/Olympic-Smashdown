using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Curse", menuName = "Upgrade/Curse", order = 0)]
public class Curse : SpecialUpgrade, IUpgrade{
    public override void Upgrade()
    {
        Player.Instance.playerStats.MaxHealth = 100000;
    }

}