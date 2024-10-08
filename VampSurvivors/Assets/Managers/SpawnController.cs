using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorMap;
    [SerializeField]
    private HashSet<Vector2Int> avalibleSpawnPositions;
    
    [SerializeField]
    CorridorFirstDungeonGenerator firstDungeonGenerator;
    
    private void Start()
    {
        firstDungeonGenerator.GenerateDungeon();

        EventManager.Instance.OnGamePlay += StartSpawnTimer;
    }

    void StartSpawnTimer()
    {
        EventManager.Instance.OnGamePlay -= StartSpawnTimer;

        avalibleSpawnPositions = firstDungeonGenerator.floorPositions;

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        //Do actual spawning and shit in here maybe waves or smth

        while (true)
        {
            Vector2Int spawnPosition = GetRandomSpawnPoint();
            UnitManager.Instance.FindEnemy(UnitType.Ghost).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
            UnitManager.Instance.FindEnemy(UnitType.Rat).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
            yield return new WaitForSeconds(0.5f);
        }
    }

    Vector2Int GetRandomSpawnPoint()
    {
        var n = Random.Range(0, avalibleSpawnPositions.Count);
        return new Vector2Int(avalibleSpawnPositions.ElementAt(n).x, avalibleSpawnPositions.ElementAt(n).y);
    }
}
