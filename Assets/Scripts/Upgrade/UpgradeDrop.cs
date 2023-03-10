using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class UpgradeDrop : MonoBehaviour
{
    [SerializeField] [ReadOnly]
    private Upgrade upgrade;

    [SerializeField]
    [ReadOnly]
    private bool canPickup = false;
    
    [HideInInspector]
    public UpgradeDropGroup UpgradeDropGroup;

    private void Awake()
    {
        upgrade = GetComponent<Upgrade>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPickup)
        {
            SFXManager.PlayMusic("pickUpgrade");
            string jsonValue = JsonUtility.ToJson(new UpgradeArgs(upgrade.upgradeData));
            EventManager.Invoke("PickUpgradeEvent", jsonValue);
            upgrade.OnUpgrade();
            if(UpgradeDropGroup != null) {
                UpgradeDropGroup.OnPickUp(); // This will destroy this upgrade drop along with others in the same group 
            } else {
                Debug.LogWarning("Upgrade Drop does not know which UpgradeGroup it belongs to", this);
                Destroy(gameObject);
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
            string jsonValue = JsonUtility.ToJson(new UpgradeArgs(upgrade.upgradeData));
            EventManager.Invoke("ShowUpgradeTextEvent", jsonValue);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
{
    if (other.gameObject.CompareTag("Player"))
    {
        canPickup = false;
        EventManager.Invoke("HideUpgradeEvent", "");
    }
}
}