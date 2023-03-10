using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDropGroup : MonoBehaviour
{
    private List<GameObject> _upgradeDrops = new(); 

    // Invoked by UpgradeDrop when it is picked up 
    public void OnPickUp() {
        foreach(var upgradeDrop in _upgradeDrops) {
            Destroy(upgradeDrop.gameObject); 
        }
        Destroy(gameObject); 
    }

    public void InstantiateUpgradeDrop(GameObject upgradeDrop_, Vector2 localPosition_) {
        GameObject upgradeDropObj = Instantiate(upgradeDrop_, this.gameObject.transform); // Instantiate new upgradeDrop as a child class
        UpgradeDrop upgradeDrop = upgradeDropObj.GetComponent<UpgradeDrop>();
        if(upgradeDrop == null) {
            Debug.LogError("upgradeDrop_ must have the <UpgradeDrop> component", upgradeDrop_); 
            Destroy(upgradeDropObj); 
            return; 
        }

        upgradeDropObj.transform.localPosition = localPosition_; 
        _upgradeDrops.Add(upgradeDropObj);
        upgradeDrop.UpgradeDropGroup = this; 
    }
}
