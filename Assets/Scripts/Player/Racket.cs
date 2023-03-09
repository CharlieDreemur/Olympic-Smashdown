using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Racket : MonoBehaviour
{
    private HashSet<Projectile> _objectsInRange;

    [Required]
    [Tooltip("The trigger collider of the racket.")]
    [SerializeField] private Collider2D _triggerCollider;


    [Header("Racket Settings")]
    [Range(0, 90)]
    [Tooltip("The angle of the hitbox of the racket.")]
    [SerializeField] private float _arcAngle = 45f;
    [SerializeField] private float _swingCooldown = 0.25f;

    public Collider2D TriggerCollider { get => _triggerCollider; private set { } }
    public float ArcAngle { get => _arcAngle; private set { } }
    public Quaternion RacketRotation { get; private set; }
    public Vector3 AimDirection { get; private set; }
    public UnityEvent swung;
    private float _triggerColliderRadius;

    private bool _canSwing = true;


    private void Awake()
    {
        _objectsInRange = new HashSet<Projectile>();
        _triggerColliderRadius = _triggerCollider.bounds.extents.x;
    }

    private void Update()
    {
        var mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        AimDirection = (mousePos - transform.position).normalized;

        if (Input.GetButtonDown("Fire1") && _canSwing)
        {
            Swing(AimDirection);
            StartCoroutine(StartSwingCooldown());
        }
    }

    public IEnumerator StartSwingCooldown()
    {
        _canSwing = false;
        yield return new WaitForSeconds(_swingCooldown);
        _canSwing = true;
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
        swung.Invoke();
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
                proj.args.DamageInfo.ownerType = ProjectileOwnerType.player;
                // var projRb = proj.GetComponent<Rigidbody2D>();
                // projRb.velocity = direction.normalized * projRb.velocity.magnitude;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _triggerColliderRadius);
        if (AimDirection != Vector3.zero)
        {
            Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, _arcAngle / 2) * (AimDirection * _triggerColliderRadius)); // right limit of the arc
            Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, -_arcAngle / 2) * (AimDirection * _triggerColliderRadius)); // left limit of the arc

            Gizmos.color = Color.green;

        }
    }
}
