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
    
    public EnemyUnit EnemyUnit;

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

        player = UnitManager.Instance.player.gameObject;

    }

    private void OnEnable()
    {
        canTakeDamage = true;
        
    }
    
    private void OnDisable()
    {
        canTakeDamage = true;
        
    }

    private void Update()
    {
        agent.SetDestination(new Vector3(player.transform.position.x, player.transform.position.y, 0));
    }

    public void SetSpeed(float newSpeed) => speed = newSpeed;

    public void TakeDamage(float damageSource)
    {
        if (!canTakeDamage) return;
        
        health -= damageSource;
        Debug.Log("ded");
            
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
        agent.isStopped = false;
        transform.position = position;
        gameObject.SetActive(true);
    }

    void Death()
    {
        agent.isStopped = true;
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
