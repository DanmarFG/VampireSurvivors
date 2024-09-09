using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public enum ProjectileType {All, Fireball}
    public enum ProjectileTeam {Friendly, Evil}

    public class ProjectileBag : MonoBehaviour
    {
        [Header("Bullet Lists")]
        public List<GameObject> allProjectiles;
        public List<GameObject> fireBallList;
        
        public GameObject FindProjectile(ProjectileType type)
        {
            List<GameObject> projectileList = GetProjectileLists(type);
            for (int b = 0; b < projectileList.Count; b++)
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

            Debug.LogWarning("Couldn't Find a Bullet");
            return null;
        }
        
        void SpawnBullets(ProjectileType type, int amount)
        {
            GameObject prefab = GetBulletPrefab(type);

            for (int i = 0; i < amount; i++)
            {
                GameObject projectile = Instantiate(prefab, transform);
                GetProjectileLists(type).Add(projectile);

                projectile.name = type.ToString() + "Bullet" + GetProjectileLists(type).Count;
                //projectile.GetComponent<Projectile>().SetUp((type != ProjectileType.unhittable)); //Set up variables & fx before disable

                projectile.SetActive(false);
                allProjectiles.Add(projectile);
            }
        }
        
        private GameObject GetBulletPrefab(ProjectileType type)
        {
            switch (type)
            {
                case ProjectileType.Fireball:
                    return null;
                    break;
            }

            return null;
        }
        
        private List<GameObject> GetProjectileLists(ProjectileType type)
        {
            switch (type)
            {
                case ProjectileType.All:
                    return allProjectiles;
                    break;
                case ProjectileType.Fireball:
                    return fireBallList;
                    break;
                default:
                    return allProjectiles;
                    break;
            }
        }
    }
}