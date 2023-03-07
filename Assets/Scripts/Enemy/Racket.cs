using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Racket : MonoBehaviour
{
    private HashSet<Projectile> _objectsInRange;

    [Required]
    [Tooltip("The trigger collider of the racket.")]
    [SerializeField] private Collider2D _triggerCollider;
    public GameObject racketGraphics;
    [Space(10)]


    [Header("Racket Settings")]
    [Range(0, 90)]
    [Tooltip("The angle of the hitbox of the racket.")]
    [SerializeField] private float arcAngle = 45f;

    public float racketGraphicRadius = 5f;
    private float _triggerColliderRadius;




    private void Awake()
    {
        _objectsInRange = new HashSet<Projectile>();
        _triggerColliderRadius = _triggerCollider.bounds.extents.x;
    }

    private void Update()
    {
        var mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        var racketDirection = (mousePos - transform.position).normalized;
        if (racketDirection != Vector3.zero)
        {
            racketGraphics.transform.position = transform.position + Quaternion.Euler(0, 0, -arcAngle / 2) * (racketDirection.normalized * racketGraphicRadius); // Set the racket to the left limit of the arc
            racketGraphics.transform.rotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, -arcAngle / 2) * racketDirection);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _triggerColliderRadius);

        var mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        var racketDirection = (mousePos - transform.position).normalized;
        if (racketDirection != Vector3.zero)
        {
            Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, arcAngle / 2) * (racketDirection.normalized * _triggerColliderRadius)); // right limit of the arc
            Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, -arcAngle / 2) * (racketDirection.normalized * _triggerColliderRadius)); // left limit of the arc

            Gizmos.color = Color.green;

        }
    }
}
