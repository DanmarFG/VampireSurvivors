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

    public GameObject bloodFX;

    public new string name = "Unit";
    public float health = 0, damage = 0, speed = 0;
    public bool canShoot = false, canTakeDamage = true;
    public bool canMove = true;

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
        if(canMove)
            agent.SetDestination(new Vector3(player.transform.position.x, player.transform.position.y, 0));
    }

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

    public void Spawn(Vector3Int position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        agent.isStopped = false;

        StartCoroutine(PositionController());
    }

    void Death()
    {
        agent.isStopped = true;
        StopAllCoroutines();


        var randomNumber = (int)Random.Range(1f, 4f);
        
        for(var i = 0; i < randomNumber; i++)
            ExperienceBag.Instance.SpawnExperience(transform.position);

        Instantiate(bloodFX, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

        gameObject.SetActive(false);
    }

    IEnumerator PositionController()
    {
        yield return new WaitForSeconds(0.5f);
        if(!UnitManager.Instance.FloorMap.HasTile(new Vector3Int((int)transform.position.x, (int)transform.position.y,0)))
        {
            gameObject.SetActive(false);
        }
    }
}
