using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NavMeshPlus.Components;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTileMap, wallTileMap;
    
    [SerializeField]
    NavMeshSurface[] navMesh;
    
    [SerializeField]
    private TileBase floorTile, wallTop; //Make random list later

    [SerializeField] private TileBase[] floorTiles;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTileMap, floorTiles);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase[] tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tileMap, floorTiles[(int)Random.Range(0, floorTiles.Length)], position);
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

        foreach(var n in navMesh)
            n.RemoveData();
    }

    public void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTileMap, wallTop, position);
    }
}
