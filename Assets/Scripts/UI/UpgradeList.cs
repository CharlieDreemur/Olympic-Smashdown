using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeList : MonoBehaviour
{
    
    public List<UpgradeData> upgrades;
    // public List<GameObject> childs;
    public GameObject buff_icon_prefab;

    void Awake(){
        EventManager.AddListener("PickUpgradeEvent", AddBuff);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddBuff(string jsonValue) {
        UpgradeArgs args = JsonUtility.FromJson<UpgradeArgs>(jsonValue);
        AddBuff(args.upgradeData);
    }

    public void AddBuff(UpgradeData upgradeData) {
        upgrades.Add(upgradeData);
        GameObject added_buff = Instantiate(buff_icon_prefab, Vector3.zero, Quaternion.identity);
        added_buff.transform.SetParent(transform);

        BuffIcon buff_icon_script = added_buff.GetComponent<BuffIcon>();
        buff_icon_script.SetData(upgradeData);

        upgrades.Add(upgradeData);
        // childs.Add(added_buff);
    }

    public void RemoveBuff(UpgradeData upgradeData) {
        upgrades.Remove(upgradeData);
        foreach (Transform child in transform) {
            Debug.Log(child.GetComponent<BuffIcon>().buff_data);
            if (child.GetComponent<BuffIcon>().buff_data.name == upgradeData.name) {
                GameObject.Destroy(child.gameObject);
                return;
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
