using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class UnitManager : MonoBehaviour
    {
        public static UnitManager Instance;
        
        [SerializeField]
        private List<GameObject> enemyData = new List<GameObject>();
        public List<GameObject> enemies = new List<GameObject>();

        public Player player;

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
        
        public void AssignPlayer(Player _player)
        {
            player = _player;
        }

        public void SpawnEnemy(string monsterName)
        {
            for (int i = 0; i < 10; i++)
            {
                var enemy = Instantiate(enemyData[0]);
                enemy.SetActive(false);
                enemies.Add(enemy);
            }
        }

        public GameObject GetEnemy(string monsterName)
        {
            foreach (var monster in enemies)
            {
                if(monster.name == monsterName && !monster.gameObject.activeInHierarchy)
                    return monster.gameObject;
            }
            SpawnEnemy(monsterName);
            return GetEnemy(monsterName);
        }
    
    }
}


