using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    [SerializeField] private float att;
    [SerializeField] private GameObject owner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            // Debug.Log("enemy");
            other.gameObject.GetComponent<IEnemy>().TakeDamage(att, 1f, owner.transform); // TODO: get player stats
        }
    }
}
