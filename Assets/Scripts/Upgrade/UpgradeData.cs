using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public enum UpgradeType{
    one,
    two,
    three
}

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Data/UpgradeData", order = 0)]
public class UpgradeData : ScriptableObject
{
    [TableList(AlwaysExpanded = true)]
    public List<UpgradeDataStruct> upgradeData = new List<UpgradeDataStruct>(){
        new UpgradeDataStruct(),
    };

    [System.Serializable]
    public struct UpgradeDataStruct
    {
        [TableColumnWidth(50, Resizable = false)]
        [PreviewField(Alignment = ObjectFieldAlignment.Center)]
        public Sprite icon;
        [TableColumnWidth(160, Resizable = false)]
        [VerticalGroup]
        public string name;
        
        [TextArea(2, 2)]
        public string description;
        [VerticalGroup] [LabelText("Type")]
        public UpgradeType upgradeType;

        public string funFact; // one fun sentence illutrate this powerup
    }
}
