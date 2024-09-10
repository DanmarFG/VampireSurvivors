using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerBox : MonoBehaviour
{
    [SerializeField]private Unit _unit;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponentInParent<Projectile>())
            return;
        
        Projectile p = other.GetComponentInParent<Projectile>();
        Debug.Log("ake damage");
        
        _unit.TakeDamage(10f);
        

        p.HitEnemy();
    }
}
