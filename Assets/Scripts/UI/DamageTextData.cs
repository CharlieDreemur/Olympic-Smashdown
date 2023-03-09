using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "DamageTextData", menuName = "Data/DamageTextData", order = 0)]
public class DamageTextData : ScriptableObject
{   
    [FoldoutGroup("Basic Setup")] [SerializeField]
    private UnityEngine.GameObject prefab;
    public UnityEngine.GameObject Prefab{get => prefab; set => prefab = value;}

    [FoldoutGroup("Basic Setup")] 
    public Color normalColor = Color.yellow; //正常状态的字体颜色

    [FoldoutGroup("Basic Setup")] 
    public int noramlFontSize = 16; //正常状态的字体大小

    [FoldoutGroup("Detailed Setup")] 
    public Color critColor = Color.red; //暴击状态的字体颜色

    [FoldoutGroup("Detailed Setup")] 
    public int critFontSize = 20; //暴击状态的字体大小
    [FoldoutGroup("Detailed Setup")] 
    public Color healColor = Color.green; //治疗状态的字体颜色

    [FoldoutGroup("Detailed Setup")] 
    public int healFontSize = 16; //治疗状态的字体大小
    [FoldoutGroup("Detailed Setup")]
    public Color abilityColor = Color.blue; //技能状态的字体颜色

    [FoldoutGroup("Detailed Setup")] 
    public int abilityFontSize = 16; //技能状态的字体大小

    [FoldoutGroup("Detailed Setup")]  
    public float moveSpeed = 30f; //字体的移动速度

    [FoldoutGroup("Detailed Setup")] 
    public float disappearTime = 1f; //字体的消失时间

    [FoldoutGroup("Detailed Setup")]
    public float disappearSpeed = 3f; //字体的消失速度
    [FoldoutGroup("Detailed Setup")]
    public float increaseScaleAmout = 1f; //字体的放大速度
    [FoldoutGroup("Detailed Setup")]
    public float decreaseScaleAmount = 1f; //字体的缩小速度
}
