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
    
    private IEnumerator Start()
    {
        firstDungeonGenerator.GenerateDungeon();

        //avalibleSpawnPositions = new List<Vector3>();

        //for (int n = floorMap.cellBounds.xMin; n < floorMap.cellBounds.xMax; n++)
        //{
        //    for (int p = floorMap.cellBounds.yMin; p < floorMap.cellBounds.yMax; p++)
        //    {
        //        Vector3Int localPlace = new Vector3Int(n, p, (int)floorMap.transform.position.y);
        //        Vector3 place = floorMap.CellToWorld(localPlace);
        //        if (floorMap.HasTile(localPlace))
        //        {
        //            avalibleSpawnPositions.Add(place);
        //        }
        //    }
        //}

        yield return new WaitForSeconds(1f);
        avalibleSpawnPositions = firstDungeonGenerator.floorPositions;

        //Vector2Int spawnPosition = GetRandomSpawnPoint();
        //UnitManager.Instance.FindEnemy(UnitType.Rat).GetComponent<Unit>().Spawn((Vector3Int)spawnPosition);


        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
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
