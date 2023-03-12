using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "AddRacketUpgrade", menuName = "Upgrade/AddRacketUpgrade", order = 0)]
public class AddRacketUpgrade : SpecialUpgrade, IUpgrade{
    public override void Upgrade()
    {
        Player.Instance.AddRacket();
    }

}