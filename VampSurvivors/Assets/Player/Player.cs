using System;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float health, maxHealth; 
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private bool canMove = true, canTakeDamage = true;

    private Rigidbody2D _rigidbody;

    private Vector2 _inputVector;

    private void Start()
    {
        health = maxHealth;
        
        _rigidbody = GetComponent<Rigidbody2D>();

        if(UnitManager.Instance)
            UnitManager.Instance.AssignPlayer(this);
        
    }

    private void Update()
    {
        _rigidbody.velocity = _inputVector * playerSpeed;
    }

    private void OnMouseDown()
    {
        TakeDamage(2);
    }

    private void OnMove(InputValue value)
    {
        if (!canMove)
        {
            _inputVector = new Vector2();
            return;
        }

        _inputVector = value.Get<Vector2>();
    }

    public void TakeDamage(float damage)
    {
        if(!canTakeDamage)
            return;
        health -= damage;
        EventManager.Instance.PlayerTookDamage(damage);
        PlayerDeath();
        StartCoroutine(DamageCoolDown());
    }

    IEnumerator DamageCoolDown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = true;
    }

    void PlayerDeath()
    {
        if (health <= 0)
        {
            StopAllCoroutines();
            EventManager.Instance.PlayerDied();
            Destroy(gameObject);
        }
    }
}
