using System;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[System.Serializable]
public struct PlayerStats
{
    public float speed, health, maxHealth, maxFov;

    public float fireballShootDelay, fireballDamage, fireballSpeed;

    public float punchDamage, punchReach;
}

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

    [Header("Animator")]
    [SerializeField] private Animator playerAnimator;
    

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

        //StartCoroutine(ShootFireball());
    }

    private void Update()
    {
        _rigidbody.velocity = _inputVector * playerSpeed;

        
        
    }

    private void OnMove(InputValue value)
    {
        if (!canMove)
        {
            _inputVector = new Vector2();
            return;
        }

        _inputVector = value.Get<Vector2>();

        if (_inputVector == Vector2.zero)
            playerAnimator.SetBool("Moving", false);
        else
            playerAnimator.SetBool("Moving", true);


    }

    private void OnPunch()
    {
        punch.Attack(playerAim.GetAimDirection());
    }

    private void OnPause()
    {
        EventManager.Instance.PauseGame();
    }

    public float GetHealth()
    {
        return health;
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

    public void LevelUp()
    {
        
        maxHealth += maxHealth * 0.1f;
        fireBallDamage += fireBallDamage * 0.1f;
        punch.damage += punch.damage * 0.1f;
    }

    public void AddStats(PlayerStats statsToAdd)
    {
        maxHealth += statsToAdd.maxHealth;
        health += statsToAdd.health;
        playerSpeed += statsToAdd.speed;
        fireBallDamage += statsToAdd.fireballDamage;

        if(GetComponentInChildren<FieldOfView>().viewAngle < 180)
        {
            GetComponentInChildren<FieldOfView>().viewAngle += statsToAdd.maxFov;
            GetComponentInChildren<Light>().spotAngle += statsToAdd.maxFov;
        }
        fireballShootDelay += statsToAdd.fireballShootDelay;
        fireBallSpeed += statsToAdd.fireballSpeed;
        punch.damage += statsToAdd.punchDamage;
        Debug.Log(statsToAdd.punchReach);

    }
}
