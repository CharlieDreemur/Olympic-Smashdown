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

    private void Awake()
    {
        upgrade = GetComponent<Upgrade>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPickup)
        {
            upgrade.OnUpgrade();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;

        }
    }
    private void OnTriggerExit(Collider other)
{
    if (other.gameObject.CompareTag("Player"))
    {
        canPickup = false;
    }
}
}