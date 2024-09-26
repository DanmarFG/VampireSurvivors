using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorMap;
    [SerializeField]
    private List<Vector3> avalibleSpawnPositions;
    
    [SerializeField]
    CorridorFirstDungeonGenerator firstDungeonGenerator;
    
    private IEnumerator Start()
    {
        firstDungeonGenerator.GenerateDungeon();
        
        avalibleSpawnPositions = new List<Vector3>();

        for (int n = floorMap.cellBounds.xMin; n < floorMap.cellBounds.xMax; n++)
        {
            for (int p = floorMap.cellBounds.yMin; p < floorMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)floorMap.transform.position.y);
                Vector3 place = floorMap.CellToWorld(localPlace);
                if (floorMap.HasTile(localPlace))
                {
                    avalibleSpawnPositions.Add(place);
                }
            }
        }
        
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Vector3Int spawnPosition = GetRandomSpawnPoint();
            UnitManager.Instance.FindEnemy(UnitType.Ghost).GetComponent<Unit>().Spawn(spawnPosition);
            yield return new WaitForSeconds(0.5f);
        }
    }

    Vector3Int GetRandomSpawnPoint()
    {
        var n = UnityEngine.Random.Range(0, avalibleSpawnPositions.Count);
        
        return new Vector3Int((int)avalibleSpawnPositions[n].x, (int)avalibleSpawnPositions[n].y);
    }
}
