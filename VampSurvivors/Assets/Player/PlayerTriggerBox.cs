using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerBox : MonoBehaviour
{
    [SerializeField] Player player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IEnemy e = collision.GetComponentInParent<IEnemy>();
        if (e != null)
            player.TakeDamage(e.GetDamage());
    }
}
