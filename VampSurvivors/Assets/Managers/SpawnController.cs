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
        EventManager.Instance.OnTenCoinsPickup += TenCoinsPickedUp;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnGamePlay -= StartSpawnTimer;
        EventManager.Instance.OnSecond -= AddSpawnCredit;
    }

    

    int timePlayed = 0;

    void AddSpawnCredit()
    {
        timePlayed++;

        if (levelCompleted)
            return;

        if(timePlayed %2 == 0)
            GiveCredits();

        if (teleporterEvent)
        {
            while (spawnCredits >= 1)
            {
                SpawnFlame();
                SpawnGhostHalf();
                SpawnRatHalf();

                if (UnitManager.Instance.spawnedEnemies > 40)
                    break;
            }
        }
        else
        {
            while (spawnCredits >= 1)
            {
                SpawnFlame();
                SpawnGhost();
                SpawnRat();

                if (UnitManager.Instance.spawnedEnemies > 40)
                    break;
            }
        }
        
    }

    void GiveCredits()
    {
        for (int i = 0; i < GameManager.Instance.IngameTimer.minutes + 1; i++)
        {
            spawnCredits += Random.Range(0, 5);
        }
    }

    void TenCoinsPickedUp()
    {
        spawnCredits += 10;
    }

    void StartSpawnTimer()
    {
        EventManager.Instance.OnGamePlay -= StartSpawnTimer;

        avalibleSpawnPositions = firstDungeonGenerator.floorPositions;

    }


    public void SpawnRat()
    {
        if(spawnCredits >= 1)
        {
            Vector2Int spawnPosition = GetRandomSpawnPoint();
            UnitManager.Instance.FindEnemy(UnitType.Rat).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);

            spawnCredits -= 1;
        }
    }

    public void SpawnRatHalf()
    {
        if (spawnCredits > 0.5f)
        {
            Vector2Int spawnPosition = GetRandomSpawnPoint();
            UnitManager.Instance.FindEnemy(UnitType.Rat).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);

            spawnCredits -= 0.5f;
        }

        
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

    public void SpawnGhostHalf()
    {
        if (spawnCredits > 2.5f)
        {
            Vector2Int spawnPosition = GetRandomSpawnPoint();
            UnitManager.Instance.FindEnemy(UnitType.Ghost).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
            spawnPosition = GetRandomSpawnPoint();
            UnitManager.Instance.FindEnemy(UnitType.Ghost).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);
            spawnCredits -= 2.5f;
        }
    }

    public void SpawnFlame()
    {
        if (spawnCredits > 6)
        {
            Vector2Int spawnPosition = GetRandomSpawnPoint();
            UnitManager.Instance.FindEnemy(UnitType.Flame).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);

            spawnCredits -= 6;
        }
    }

    Vector2Int GetRandomSpawnPoint()
    {
        var n = Random.Range(1, avalibleSpawnPositions.Count-1);
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
        yield return new WaitForSeconds(20f);
        StopSpawning();
        EventManager.Instance.FinishedLadderEvent();
    }
}
