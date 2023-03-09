using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class MockEnemyAI : MonoBehaviour
{
    [SerializeField] private float _speed;
    public UnityEvent died;
    private Rigidbody2D _rb;
    private GameObject _player;

    private void Awake()
    {
        _player = Player.Instance.gameObject;
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

    [Button("Kill")]
    public void Kill()
    {
        died.Invoke();
        Destroy(gameObject);
    }
}
