using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Managers
{
    public enum UnitType {All, Rat, Ghost}
    public class UnitManager : MonoBehaviour
    {
        public static UnitManager Instance;
        
        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        [SerializeField] private Tilemap floorMap;
        public Tilemap FloorMap => floorMap;
        
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
        public List<GameObject> ratList = new List<GameObject>();
        public List<GameObject> ghostList = new List<GameObject>();

        public Player player;
        public GameObject playerPF;

        public int spawnedEnemies = 0;
        
        private void Start()
        {
            EventManager.Instance.OnNavMeshBuilt += OnNavmeshFinishedBuilding;
            EventManager.Instance.OnGamePlay += SpawnPlayer;
        }

        private void OnDisable()
        {
            EventManager.Instance.OnNavMeshBuilt -= OnNavmeshFinishedBuilding;
        }

        void SpawnPlayer()
        {
            if (player == null)
            {
                Instantiate(playerPF, new Vector3(0, 0, 0), Quaternion.identity);
            }

            EventManager.Instance.OnGamePlay -= SpawnPlayer;
        }

        void OnNavmeshFinishedBuilding()
        {
            CreateEnemy(UnitType.Rat, 20);
            CreateEnemy(UnitType.Ghost, 20);
        }
        
        
        
        public GameObject FindEnemy(UnitType type)
        {
            var projectileList = GetEnemyLists(type);
            for (var i = 0; i < projectileList.Count; i++)
            {
                if (projectileList[i].activeSelf == false)
                {
                    return projectileList[i];
                }

                if (i == projectileList.Count - 1)
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
                UnitType.Rat => enemyPrefabs[0],
                UnitType.Ghost => enemyPrefabs[1],
                _ => null
            };
        }
        
        private List<GameObject> GetEnemyLists(UnitType type)
        {
            return type switch
            {
                UnitType.All => allEnemies,
                UnitType.Rat => ratList,
                UnitType.Ghost => ghostList,
                _ => allEnemies
            };
        }

        public IEnumerator AssignPlayer(Player _player)
        {
            player = _player;
            yield return null;

            virtualCamera.Follow = player.gameObject.transform;
        }

        public Vector2 GetPlayerPosition()
        {
            if(player == null)
                return Vector2.zero;
            if(player.GetHealth() <= 0)
                return Vector2.zero;

            return player.transform.position;
        }
    }
}


