using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Unity.VisualScripting;
using UnityEngine.AI;


public class Unit : MonoBehaviour
{
    [SerializeField]NavMeshAgent agent;
    [SerializeField]SpriteRenderer spriteRenderer;
    
    GameObject player;
    
    Rigidbody2D rb;
    
    public EnemyUnit EnemyUnit;

    public float runSpeed;

    public new string name = "Unit";
    public float health = 0, damage = 0, speed = 0;
    public bool canShoot = false, canTakeDamage = true;

    private void Start()
    {
        name = EnemyUnit.name;
        health = EnemyUnit.health;
        damage = EnemyUnit.damage;
        speed = EnemyUnit.speed;
        canShoot = EnemyUnit.canShoot;

        spriteRenderer.sprite = EnemyUnit.sprite;
        
        agent.updatePosition = true;
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        agent.speed = speed;
        runSpeed = speed / 2f;

        player = UnitManager.Instance.player.gameObject;
        rb = player.GetComponent<Rigidbody2D>();

    }

    private void OnEnable()
    {
        canTakeDamage = true;
        agent.enabled = true;
        agent.speed = speed;
    }
    
    private void OnDisable()
    {
        canTakeDamage = true;
        
    }

    private void Update()
    {
        agent.SetDestination(new Vector3(player.transform.position.x, player.transform.position.y, 0));
    }

    private bool looking = true;
    private Coroutine e;
    
    public void LookingAtPlayer()
    {
        agent.speed = runSpeed;

        if(e != null)
            StopCoroutine(e);
        
        e = StartCoroutine(ResetSpeed());
    }

    public IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(0.1f);
        agent.speed = speed;
    }

    public void SetSpeed(float newSpeed) => speed = newSpeed;

    public void TakeDamage(float damageSource)
    {
        if (!canTakeDamage) return;
        
        health -= damageSource;
            
        StartCoroutine(EnemyTakeDamageCooldown());
        
        if(health <= 0){
            Death();
        }
    }

    IEnumerator EnemyTakeDamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.1f);
        canTakeDamage = true;
    }

    public void Spawn(Vector2 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        agent.isStopped = false;
    }

    void Death()
    {
        agent.isStopped = true;
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
