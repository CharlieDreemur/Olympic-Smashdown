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
    [MinValue(0.1)]
    public int racketRange = 1;



}
