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

    double spawnCredits = 0;
    
    private void Start()
    {
        firstDungeonGenerator.GenerateDungeon();

        EventManager.Instance.OnGamePlay += StartSpawnTimer;
        EventManager.Instance.OnSecond += AddSpawnCredit;
        EventManager.Instance.OnStartLadderEvent += StartTeleporterEvent;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnGamePlay -= StartSpawnTimer;
        EventManager.Instance.OnSecond -= AddSpawnCredit;
    }

    void AddSpawnCredit()
    {
        spawnCredits += (0.1f * (1 + 0.4 * GameManager.Instance.coef) * 1/ 2);
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
            yield return new WaitForSeconds(1f);

            if (UnitManager.Instance.spawnedEnemies > 40)
                continue;

            while (spawnCredits > 5 && UnitManager.Instance.spawnedEnemies < 40)
            {
                SpawnGhost();
                SpawnRat();
            }
        }
    }

    public void SpawnRat()
    {
        if(spawnCredits > 5)
        {
            Vector2Int spawnPosition = GetRandomSpawnPoint();
            UnitManager.Instance.FindEnemy(UnitType.Rat).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
            UnitManager.Instance.FindEnemy(UnitType.Rat).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
            UnitManager.Instance.FindEnemy(UnitType.Rat).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
            UnitManager.Instance.FindEnemy(UnitType.Rat).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);

            spawnCredits -= 5;
        }
    }

    public void SpawnRatFree()
    {
        Vector2Int spawnPosition = GetRandomSpawnPoint();
        UnitManager.Instance.FindEnemy(UnitType.Rat).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
        UnitManager.Instance.FindEnemy(UnitType.Rat).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
        UnitManager.Instance.FindEnemy(UnitType.Rat).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
        UnitManager.Instance.FindEnemy(UnitType.Rat).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
    }

    public void SpawnGhost()
    {
        if (spawnCredits > 10)
        {
            Vector2Int spawnPosition = GetRandomSpawnPoint();
            UnitManager.Instance.FindEnemy(UnitType.Ghost).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
            UnitManager.Instance.FindEnemy(UnitType.Ghost).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
            UnitManager.Instance.FindEnemy(UnitType.Ghost).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
            UnitManager.Instance.FindEnemy(UnitType.Ghost).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);

            spawnCredits -= 10;
        }
    }

    public void SpawnGhostFree()
    {
        Vector2Int spawnPosition = GetRandomSpawnPoint();
        UnitManager.Instance.FindEnemy(UnitType.Ghost).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
        UnitManager.Instance.FindEnemy(UnitType.Ghost).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
        UnitManager.Instance.FindEnemy(UnitType.Ghost).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
        UnitManager.Instance.FindEnemy(UnitType.Ghost).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
    }

    Vector2Int GetRandomSpawnPoint()
    {
        var n = Random.Range(0, avalibleSpawnPositions.Count);
        return new Vector2Int(avalibleSpawnPositions.ElementAt(n).x, avalibleSpawnPositions.ElementAt(n).y);
    }

    void StartTeleporterEvent()
    {
        StartCoroutine(SpawnEnemyTeleporter());
        EventManager.Instance.OnStartLadderEvent -= StartTeleporterEvent;
    }

    void StopSpawning()
    {
        StopAllCoroutines();
    }

    IEnumerator SpawnEnemyTeleporter()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (UnitManager.Instance.spawnedEnemies > 40)
                continue;

            SpawnGhostFree();
            SpawnRatFree();


        }
    }

    IEnumerator TeleporterEventTimer()
    {
        yield return new WaitForSeconds(30f);
        StopSpawning();
        EventManager.Instance.FinishedLadderEvent();
    }


}
