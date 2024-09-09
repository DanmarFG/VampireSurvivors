using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Projectile>())
            return;
        
        Projectile p = other.GetComponent<Projectile>();
        GetComponentInParent<Unit>().health -= p.damage;

        p.HitEnemy();
    }
}
