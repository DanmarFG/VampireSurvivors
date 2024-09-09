using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float Speed;
    protected float TimeAlive;

    public float damage;

    private Vector2 _dir;
    public float lifeTime;

    void OnDisable()
    {
        ResetBullet();
        CancelInvoke();
    }

    void ResetBullet()
    {
        
    }
    
    public void SetDamage(float newDamage) => damage = newDamage;
    public void SetMoveDir(Vector3 newDir) => _dir = newDir;
    public void SetSpeed(float newSpeed) => Speed = newSpeed;
    
    public void ShootProjectile(Vector3 direction, Vector2 startingPosition)
    {
        gameObject.SetActive(true);
        
        transform.position = startingPosition;
        transform.up = direction;
        
        SetMoveDir(transform.up);

        GetComponent<Rigidbody2D>().AddForce(_dir * Speed);

        Invoke(nameof(Deactivate), lifeTime);
        
        
    }

    public void HitEnemy()
    {
        Deactivate();
    }

    void Deactivate()
    {
        ResetBullet();

        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
