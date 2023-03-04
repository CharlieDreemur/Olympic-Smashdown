using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "DamageTextData", menuName = "TheMonsterArmy/ScriptableObject/UISystem/DamageTextData", order = 0)]
public class DamageTextData : ScriptableObject
{   
    [FoldoutGroup("Basic Setup")] [HideLabel] [SerializeField]
    private UnityEngine.GameObject prefab;
    public UnityEngine.GameObject Prefab{get => prefab; set => prefab = value;}

    [FoldoutGroup("Basic Setup")] [HideLabel]
    public Color normalColor = Color.yellow; //正常状态的字体颜色

    [FoldoutGroup("Basic Setup")] [HideLabel]
    public int noramlFontSize = 16; //正常状态的字体大小

    [FoldoutGroup("Detailed Setup")] [HideLabel]
    public Color critColor = Color.red; //暴击状态的字体颜色

    [FoldoutGroup("Detailed Setup")] [HideLabel]
    public int critFontSize = 20; //暴击状态的字体大小
    [FoldoutGroup("Detailed Setup")] [HideLabel] 
    public Color healColor = Color.green; //治疗状态的字体颜色

    [FoldoutGroup("Detailed Setup")] [HideLabel] 
    public int healFontSize = 16; //治疗状态的字体大小
    [FoldoutGroup("Detailed Setup")] [HideLabel] 
    public Color abilityColor = Color.blue; //技能状态的字体颜色

    [FoldoutGroup("Detailed Setup")] [HideLabel]
    public int abilityFontSize = 16; //技能状态的字体大小

    [FoldoutGroup("Detailed Setup")] [HideLabel] 
    public float moveSpeed = 30f; //字体的移动速度

    [FoldoutGroup("Detailed Setup")] [HideLabel] 
    public float disappearTime = 1f; //字体的消失时间

    [FoldoutGroup("Detailed Setup")] [HideLabel] 
    public float disappearSpeed = 3f; //字体的消失速度
}
