using UnityEngine;

public class ObstacleTileMap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        Player p = collision.GetComponentInParent<Player>();
        
        p.AllowInterraction(true);        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        Player p = collision.GetComponentInParent<Player>();

        p.AllowInterraction(false);
    }
}
