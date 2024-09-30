using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Punch : MonoBehaviour
{
    [SerializeField] private float attackUptime = 0.1f;

    public float damage;
    public void SetDamage(float newDamage) => damage = newDamage;
    
    public void Attack(Vector2 aimDir)
    {
        if(gameObject.activeInHierarchy)
            return;

        gameObject.SetActive(true);
        StartCoroutine(DisableAttack());
    }

    IEnumerator DisableAttack()
    {
        yield return new WaitForSeconds(attackUptime);
        gameObject.SetActive(false);
    }
    
    
}
