using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    private HashSet<GameObject> _objectsInRange;
    
    public GameObject racketGraphics;
    public float racketRadius = 5f;
    private void Update()
    {
        var mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        var racketDirection = (mousePos - transform.position).normalized;
        if (racketDirection != Vector3.zero)
        {
            racketGraphics.transform.position = transform.position + racketDirection.normalized * racketRadius;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            Swing(racketDirection);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // tracking enemy now
        // will change to projectiles later
        if (other.GetComponent<Enemy>() != null)
        {
            _objectsInRange.Add(other.gameObject);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            _objectsInRange.Remove(other.gameObject);
        }
    }

    public void Swing(Vector2 direction)
    {
        // test angles and apply swing here
        Debug.Log("Swing");
    }
}
