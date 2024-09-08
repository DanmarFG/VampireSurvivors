using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;


public class Unit : MonoBehaviour
{
    public EnemyUnit EnemyUnit;

    public new string name = "Unit";
    public float health = 0, damage = 0, speed = 0;
    public bool canShoot = false;

    private void Start()
    {
        name = EnemyUnit.name;
        health = EnemyUnit.health;
        damage = EnemyUnit.damage;
        speed = EnemyUnit.speed;
        canShoot = EnemyUnit.canShoot;

        GetComponentInChildren<SpriteRenderer>().sprite = EnemyUnit.sprite;
    }


}
