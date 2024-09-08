using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "EnemiySO")]
public class EnemyUnit : ScriptableObject
{
    public new string name = "Unit";
    public float health = 0, damage = 0, speed = 0;
    public bool canShoot = false;

    public Sprite sprite;
}
