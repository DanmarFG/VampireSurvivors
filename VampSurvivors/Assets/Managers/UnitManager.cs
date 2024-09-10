using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public enum UnitType {All, Bat, SkellyBoi}
    public class UnitManager : MonoBehaviour
    {
        public static UnitManager Instance;

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
        
        [SerializeField]
        private List<GameObject> enemyPrefabs = new List<GameObject>();

        public List<GameObject> allEnemies = new List<GameObject>();
        public List<GameObject> batList = new List<GameObject>();

        public Player player;
        
        private void Start()
        {
            CreateEnemy(UnitType.Bat, 20);
        }
        
        public GameObject FindEnemy(UnitType type)
        {
            var projectileList = GetEnemyLists(type);
            for (var b = 0; b < projectileList.Count; b++)
            {
                if (projectileList[b].activeSelf == false)
                {
                    return projectileList[b];
                }

                if (b == projectileList.Count - 1)
                {
                    CreateEnemy(type, 10);
                }
            }

            return  FindEnemy(type);
        }
        
        private void CreateEnemy(UnitType type, int amount)
        {
            var prefab = GetEnemyPrefab(type);

            for (var i = 0; i < amount; i++)
            {
                var enemy = Instantiate(prefab, transform);
                GetEnemyLists(type).Add(enemy);

                enemy.name = type.ToString() + GetEnemyLists(type).Count;
                
                enemy.SetActive(false);
                allEnemies.Add(enemy);
            }
        }
        
        private GameObject GetEnemyPrefab(UnitType type)
        {
            return type switch
            {
                UnitType.Bat => enemyPrefabs[0],
                _ => null
            };
        }
        
        private List<GameObject> GetEnemyLists(UnitType type)
        {
            return type switch
            {
                UnitType.All => allEnemies,
                UnitType.Bat => batList,
                _ => allEnemies
            };
        }

        public void AssignPlayer(Player _player)
        {
            player = _player;
        }
    }
}


