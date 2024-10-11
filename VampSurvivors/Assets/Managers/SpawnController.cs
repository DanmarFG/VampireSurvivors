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

    public double spawnCredits = 0;

    public bool teleporterEvent = false, levelCompleted = false;
    
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
        if (levelCompleted)
            return;

        spawnCredits += (0.1f * (1 + 0.4 * GameManager.Instance.coef) * 1/ 2);

        if (teleporterEvent)
        {
            while (spawnCredits >= 1)
            {
                SpawnGhostFree();
                SpawnRatFree();

                if (UnitManager.Instance.spawnedEnemies > 40)
                    break;
            }
        }
        else
        {
            while (spawnCredits >= 1)
            {
                SpawnGhost();
                SpawnRat();

                if (UnitManager.Instance.spawnedEnemies > 40)
                    break;
            }
        }
        
    }

    void StartSpawnTimer()
    {
        EventManager.Instance.OnGamePlay -= StartSpawnTimer;

        avalibleSpawnPositions = firstDungeonGenerator.floorPositions;

    }


    public void SpawnRat()
    {
        if(spawnCredits > 1)
        {
            Vector2Int spawnPosition = GetRandomSpawnPoint();
            UnitManager.Instance.FindEnemy(UnitType.Rat).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);

            spawnCredits -= 1;
        }
    }

    public void SpawnRatFree()
    {
        Vector2Int spawnPosition = GetRandomSpawnPoint();
        UnitManager.Instance.FindEnemy(UnitType.Rat).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
    }

    public void SpawnGhost()
    {
        if (spawnCredits > 5)
        {
            Vector2Int spawnPosition = GetRandomSpawnPoint();
            UnitManager.Instance.FindEnemy(UnitType.Ghost).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);

            spawnCredits -= 5;
        }
    }

    public void SpawnGhostFree()
    {
        Vector2Int spawnPosition = GetRandomSpawnPoint();
        UnitManager.Instance.FindEnemy(UnitType.Ghost).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
        spawnPosition = GetRandomSpawnPoint();
        UnitManager.Instance.FindEnemy(UnitType.Ghost).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
    }

    Vector2Int GetRandomSpawnPoint()
    {
        var n = Random.Range(0, avalibleSpawnPositions.Count);
        return new Vector2Int(avalibleSpawnPositions.ElementAt(n).x, avalibleSpawnPositions.ElementAt(n).y);
    }

    void StartTeleporterEvent()
    {
        teleporterEvent = true;
        EventManager.Instance.OnStartLadderEvent -= StartTeleporterEvent;

        StartCoroutine(TeleporterEventTimer());
    }

    void StopSpawning()
    {
        StopAllCoroutines();
    }

    IEnumerator TeleporterEventTimer()
    {
        yield return new WaitForSeconds(5f);
        StopSpawning();
        EventManager.Instance.FinishedLadderEvent();
    }
}
