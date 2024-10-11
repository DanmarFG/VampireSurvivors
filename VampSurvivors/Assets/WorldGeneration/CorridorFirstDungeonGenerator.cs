using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using NavMeshPlus.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float roomPercent = 0.8f;
    
    [SerializeField]
    NavMeshSurface[] navMesh;

    public HashSet<Vector2Int> floorPositions;
    public HashSet<Vector2Int> obstaclePositions;



    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();

        StartCoroutine(BuildNavmesh());
    }

    IEnumerator BuildNavmesh()
    {

        navMesh[^1].OnNavmeshFinishedBuilding += Finished;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        foreach(var n in navMesh)
            n.BuildNavMesh();

    }

    private void Finished()
    {
        navMesh[^1].OnNavmeshFinishedBuilding-= Finished;
        EventManager.Instance.NavMeshFinished();
    }

    private void CorridorFirstGeneration()
    {
        floorPositions = new HashSet<Vector2Int>();
        obstaclePositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        
        CreateCorridors(floorPositions, potentialRoomPositions);
        
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);
        
        floorPositions.UnionWith(roomPositions);
        
        tileMapVisualizer.PaintFloorTiles(floorPositions);

        foreach(var pos in floorPositions)
        {
            if(UnityEngine.Random.Range(0, 10) == 5)
                obstaclePositions.Add(pos);
        }

        int i = 0;
        int rand = UnityEngine.Random.Range(0, floorPositions.Count-1);

        foreach (var pos in floorPositions)
        {
            if(i == rand)
            {
                tileMapVisualizer.PlaceLadder(pos);
                break;
            }
            i++;
        }

        foreach (var pos in obstaclePositions)
            tileMapVisualizer.PaintSingleObstacle(pos);

        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if (roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighboursCount++;
                }
            }
            if(neighboursCount == 1)
                deadEnds.Add(position);
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);
        
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProcedualGenerationAlgorithm.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[^1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
            
        }
    }
}
