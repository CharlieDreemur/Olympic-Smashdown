using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffList : MonoBehaviour
{
    
    public List<UpgradeData.UpgradeDataStruct> buffs;
    public List<GameObject> childs;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBuff(UpgradeData.UpgradeDataStruct buff_data_) {
        buffs.Add(buff_data_);

    }

    public void RemoveBuff(UpgradeData.UpgradeDataStruct buff_data_) {
        buffs.Remove(buff_data_);
        
    }

    private void UpdateUI() {

    }
}
