using Managers;
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
    [SerializeField]
    bool canMove = true;

    Rigidbody2D _rigidbody;

    Vector2 inputVector;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if(UnitManager.Instance)
        UnitManager.Instance.AssignPlayer(this);
    }

    private void Update()
    {
        _rigidbody.velocity = inputVector * _playerSpeed;
    }

    void OnMove(InputValue value)
    {
        if (!canMove)
        {
            inputVector = new Vector2();
            return;
        }

        inputVector = value.Get<Vector2>();
    }
}
