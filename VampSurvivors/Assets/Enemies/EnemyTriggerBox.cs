using UnityEngine;

public class EnemyTriggerBox : MonoBehaviour
{
    [SerializeField]private Unit _unit;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Punch>())
        {
            _unit.TakeDamage(other.GetComponent<Punch>().damage);
        }

        if (other.GetComponentInParent<Projectile>())
        {
            Projectile p = other.GetComponentInParent<Projectile>();
            _unit.TakeDamage(p.damage);
            p.HitEnemy(); 
        }
        
        
    }
}
