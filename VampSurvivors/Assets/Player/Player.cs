using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    float health, maxHealth;
    [SerializeField]
    float _playerSpeed;

    Rigidbody2D _rigidbody;

    Vector2 inputVector;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidbody.velocity = inputVector * _playerSpeed;
    }

    void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }
}
