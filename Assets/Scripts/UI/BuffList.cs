using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffList : MonoBehaviour
{
    
    public List<UpgradeData> buffs;
    // public List<GameObject> childs;
    public GameObject buff_icon_prefab;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBuff(UpgradeData buff_data_) {
        buffs.Add(buff_data_);
        GameObject added_buff = Instantiate(buff_icon_prefab, Vector3.zero, Quaternion.identity);
        added_buff.transform.SetParent(transform);

        BuffIcon buff_icon_script = added_buff.GetComponent<BuffIcon>();
        buff_icon_script.SetData(buff_data_);

        buffs.Add(buff_data_);
        // childs.Add(added_buff);
    }

    public void RemoveBuff(UpgradeData buff_data_) {
        buffs.Remove(buff_data_);
        foreach (Transform child in transform) {
            if (child.GetComponent<BuffIcon>().buff_data.name == buff_data_.name) {
                GameObject.Destroy(child);
            }
        }
    }

    private void RemoveChilds() {
        // clear all the child buff list.
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
