using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Racket : MonoBehaviour
{
    private HashSet<Projectile> _objectsInRange;
    
    public GameObject racketGraphics;
    public float racketRadius = 5f;

    private void Awake()
    {
        _objectsInRange = new HashSet<Projectile>();
    }

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
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        var proj = col.GetComponent<Projectile>();
        if (proj != null)
        {
            _objectsInRange.Add(proj);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        var proj = other.GetComponent<Projectile>();
        if (proj != null && _objectsInRange.Contains(proj))
        {
            _objectsInRange.Remove(proj);
        }
    }

    private void Swing(Vector2 direction)
    {
        foreach (var proj in _objectsInRange)
        {
            Debug.Log(proj);
            var projDir3 = (proj.gameObject.transform.position - transform.position);
            var projDir2 = new Vector2(projDir3.x, projDir3.y).normalized;
           // test direction
           // 0.707 is like 45 deg
           if (Vector2.Dot(direction.normalized, projDir2) > 0.707f)
           {
               proj.args.direction = direction.normalized;
               // var projRb = proj.GetComponent<Rigidbody2D>();
               // projRb.velocity = direction.normalized * projRb.velocity.magnitude;
           }
        }
    }
}
