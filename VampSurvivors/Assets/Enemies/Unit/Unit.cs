using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;


public class Unit : MonoBehaviour
{
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

        GetComponentInChildren<SpriteRenderer>().sprite = EnemyUnit.sprite;
    }

    public void TakeDamage(float damageSource)
    {
        if (canTakeDamage)
        {
            health -= damageSource;
            StartCoroutine(enemyTakeDamageCooldown());
        }
        
        if(health <= 0)
            EnemyDeath();
    }

    IEnumerator enemyTakeDamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.1f);
        canTakeDamage = true;
    }

    void EnemyDeath()
    {
        gameObject.SetActive(false);
    }


}
