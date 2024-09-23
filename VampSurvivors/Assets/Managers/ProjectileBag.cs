using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public enum ProjectileType {All, Fireball, IceBall}
    public enum ProjectileTeam {Friendly, Evil}

    public class ProjectileBag : MonoBehaviour
    {
        public static ProjectileBag Instance;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        [Header("Prefabs")]
        [SerializeField] private GameObject _fireballPf;
        
        [Header("Bullet Lists")]
        public List<GameObject> AllProjectiles;
        public List<GameObject> FireBallList;

        private void Start()
        {
            SpawnBullets(ProjectileType.Fireball, 20);
        }

        public GameObject FindProjectile(ProjectileType type)
        {
            var projectileList = GetProjectileLists(type);
            for (var b = 0; b < projectileList.Count; b++)
            {
                if (projectileList[b].activeSelf == false)
                {
                    return projectileList[b];
                }

                if (b == projectileList.Count - 1)
                {
                    SpawnBullets(type, 10);
                }
            }

            return  FindProjectile(type);
        }
        
        private void SpawnBullets(ProjectileType type, int amount)
        {
            var prefab = GetProjectilePrefab(type);

            for (var i = 0; i < amount; i++)
            {
                var projectile = Instantiate(prefab, transform);
                GetProjectileLists(type).Add(projectile);

                projectile.name = type.ToString() + GetProjectileLists(type).Count;
                
                projectile.SetActive(false);
                AllProjectiles.Add(projectile);
            }
        }
        
        private GameObject GetProjectilePrefab(ProjectileType type)
        {
            return type switch
            {
                ProjectileType.Fireball => _fireballPf,
                _ => null
            };
        }
        
        private List<GameObject> GetProjectileLists(ProjectileType type)
        {
            return type switch
            {
                ProjectileType.All => AllProjectiles,
                ProjectileType.Fireball => FireBallList,
                _ => AllProjectiles
            };
        }
    }
}