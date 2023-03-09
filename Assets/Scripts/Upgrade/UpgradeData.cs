using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "UpgradeData", menuName = "Data/UpgradeData", order = 0)]
public class UpgradeData : ScriptableObject
{
    public Sprite icon;

    public new string name;

    [TextArea(2, 2)]
    public string description;
    public string funFact; // one fun sentence illutrate this powerup //"wtf is this?" BY Charlie

    public PlayerStats statsBonus;
    public SpecialUpgrade specialUpgrade;


}
