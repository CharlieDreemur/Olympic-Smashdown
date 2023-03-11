using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Healing", menuName = "Upgrade/Healing", order = 0)]
public class Healing : SpecialUpgrade, IUpgrade
{
    public int healNumber = 1;
    public float timeInterval = 20f;
    private float time = 0f;
    public override void OnUpdate()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            Player.Instance.playerStats.CurrentHealth += healNumber;
            time = timeInterval;
        }
    }

}