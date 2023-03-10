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

        // Use raycast to =make sure the upgradeDrop is not spawned inside an obstacle
        Vector2 direction = localPosition_.normalized;
        float distance = localPosition_.magnitude;
        RaycastHit2D hit = Physics2D.Linecast(transform.position, (Vector2)transform.position + localPosition_, LayerMask.GetMask("Obstacle")); // TODO: Make this layermask a variable
        if ( hit.collider != null) { // TODO: Make this layermask a variable
            // Debug.Log("Hit: " + hit.collider.name);
            localPosition_ = hit.point - (Vector2)transform.position;
        } else {
            // Debug.Log("No hit");
            upgradeDropObj.transform.localPosition = localPosition_; 
        }

        _upgradeDrops.Add(upgradeDropObj);
        upgradeDrop.UpgradeDropGroup = this; 
    }
}
