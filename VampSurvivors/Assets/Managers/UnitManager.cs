using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
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
    
        [Serializable]
        public class UnitData
        {
            public string name;
            public float health, damage, speed;
            public bool canShoot;
        }
    
        [Serializable]
        public class EnemyList
        {
            public UnitData[] enemies;
        }

        public EnemyList enemyTypes = new EnemyList();
        private void Start()
        {
            enemyTypes = JsonUtility.FromJson<EnemyList>(Resources.Load<TextAsset>("Enemies/EnemyData").text);
        }
    
    }
}


