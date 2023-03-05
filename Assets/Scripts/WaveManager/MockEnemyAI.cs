using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MockEnemyAI : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody2D _rb;
    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null)
        {
            Debug.LogError("Player not found");
        }

        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = _player.transform.position;
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();
        _rb.MovePosition(transform.position + direction * _speed * Time.deltaTime);
    }
}
