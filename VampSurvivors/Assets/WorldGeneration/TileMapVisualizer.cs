using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NavMeshPlus.Components;
using UnityEngine.UIElements;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTileMap, wallTileMap, obstacleTileMap;
    
    [SerializeField]
    NavMeshSurface[] navMesh;
    
    [SerializeField]
    private TileBase wallTop, chest, ladderTile; //Make random list later

    [SerializeField] private TileBase[] floorTiles, obstacleTiles;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTileMap, floorTiles);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase[] tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tileMap, floorTiles[Random.Range(0, floorTiles.Length)], position);
        }
       
    }

    private void PaintSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
        obstacleTileMap.ClearAllTiles();

        foreach (var n in navMesh)
            n.RemoveData();
    }

    public void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTileMap, wallTop, position);
    }

    int chests = 0;

    public void PaintSingleObstacle(Vector2Int position)
    {
        if (Random.Range(0, 10) > 8)
        {
            PaintSingleChest(position);
            chests++;
        }
        else
        {
            PaintSingleTile(obstacleTileMap, obstacleTiles[Random.Range(0, obstacleTiles.Length)], position);
        }
    }

    public void PlaceLadder(Vector2Int position)
    {
        PaintSingleTile(obstacleTileMap, ladderTile, position);
    }

    public void PaintSingleChest(Vector2Int position)
    {
        PaintSingleTile(obstacleTileMap, chest, position);
    }
}
