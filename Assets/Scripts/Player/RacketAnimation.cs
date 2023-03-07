using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Racket))]
public class RacketAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    [Required]
    [SerializeField] private AnimationCurve _animationCurve;
    [Range(0, 2)]
    [SerializeField] private float _swingDuration = 0.5f;

    [Header("Object References")]
    [Required]
    [SceneObjectsOnly]
    [SerializeField] GameObject _racketObject;


    private Racket _racket;
    private bool _isSwinging = false;
    private float _swingTimer = 0f;
    private Quaternion _racketRotation;
    private float _racketGraphicRadius;
    private void Awake()
    {
        _racket = GetComponent<Racket>();
        _racket.swung.AddListener(PlayAnimation);
        _racketGraphicRadius = _racket.TriggerCollider.bounds.extents.x;
    }

    private void Update()
    {
        var mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        var aimDirection = (mousePos - transform.position).normalized;
        if (aimDirection != Vector3.zero)
        {
            // _racketObject.transform.position = transform.position + Quaternion.Euler(0, 0, -_racket.ArcAngle / 2) * (aimDirection.normalized * _racketGraphicRadius); // Set the racket to the left limit of the arc
            _racketRotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, -_racket.ArcAngle / 2) * aimDirection);
            _racketObject.transform.rotation = _racketRotation;
        }

        if (_isSwinging)
        {
            _swingTimer -= Time.deltaTime;
            _racketObject.transform.rotation = Quaternion.Euler(0, 0, _animationCurve.Evaluate(1 - _swingTimer / _swingDuration) * _racket.ArcAngle) * _racketRotation;

            if (_swingTimer <= 0)
            {
                _isSwinging = false;
            }
        }
    }

    public void PlayAnimation()
    {
        _isSwinging = true;
        _swingTimer = _swingDuration;
    }
}
