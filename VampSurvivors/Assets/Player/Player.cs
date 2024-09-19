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

    [SerializeField] private PlayerAim playerAim;
    
    [Header("Fireball")]
    [SerializeField] private float fireballShootDelay = 1f;
    [SerializeField] private float fireBallSpeed = 200f;
    [SerializeField] private float fireBallDamage = 200f;
    
    [Header("Active Sword Attack")]
    [SerializeField] private Punch punch;
    [SerializeField] private float attackDowntime = 2f;
    

    private Rigidbody2D _rigidbody;

    private Vector2 _inputVector;

    private IEnumerator Start()
    {
        health = maxHealth;
        
        _rigidbody = GetComponent<Rigidbody2D>();

        if (UnitManager.Instance)
        {
            StartCoroutine(UnitManager.Instance.AssignPlayer(this));
        }
            
        yield return null;
    }

    private void Update()
    {
        _rigidbody.velocity = _inputVector * playerSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExperienceBag.Instance.SpawnExperience(transform.position);
        }
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

    private void OnPunch()
    {
        punch.Attack(playerAim.GetAimDirection());
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
        if (!(health <= 0)) return;
        
        StopAllCoroutines();
        EventManager.Instance.PlayerDied();
        Destroy(gameObject);
    }

    private IEnumerator SwordStrike()
    {
        
        yield return new WaitForSeconds(attackDowntime);
    }

    private IEnumerator ShootFireball()
    {
        while (true)
        {
            var shot = ProjectileBag.Instance.FindProjectile(Managers.ProjectileType.Fireball);
            var projectile = shot.GetComponent<Projectile>();
            
            projectile.SetSpeed(fireBallSpeed);
            projectile.SetDamage(fireBallDamage);
            projectile.ShootProjectile(playerAim.GetAimDirection(), transform.position);

            yield return new WaitForSeconds(fireballShootDelay);
        }
    }
}
