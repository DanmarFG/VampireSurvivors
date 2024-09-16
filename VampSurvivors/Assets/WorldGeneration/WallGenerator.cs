using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
   public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileMapVisualizer tileMapVisualizer)
   {
      var basicWallPositions = FindWallsDirections(floorPositions, Direction2D.cardinalDirectionsList);
      foreach (var position in basicWallPositions)
      {
         tileMapVisualizer.PaintSingleBasicWall(position);
      }
   }

   private static HashSet<Vector2Int> FindWallsDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
   {
      HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

      foreach (var position in floorPositions)
      {
         foreach (var direction in directionsList)
         {
            var neighborPositions = position + direction;
            if (!floorPositions.Contains(neighborPositions))
            {
               wallPositions.Add(neighborPositions);
            }
         }
      }
      
      return wallPositions;
   }
}
