using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint2> grid = new Dictionary<Vector2Int, Waypoint2>();
    Dictionary<Vector2Int, Waypoint2> path = new Dictionary<Vector2Int, Waypoint2>();
    GameObject myTop;
    [SerializeField] Waypoint2 StartWaypoint, EndWaypoint;
    [SerializeField] Color ColorStart, ColorEnd, ColorExploration, ColorPath;
    [SerializeField] bool isRunning = true;
    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
    Queue<Waypoint2> queueOfWaypoints = new Queue<Waypoint2>();
    List<Waypoint2> Path = new List<Waypoint2>();
    Waypoint2 searchCenter;
    

    void Start()
    {
        LoadBlocks();
        Pathfind();
        ColorStartAndEnd();
    }

    void Pathfind()
    {
        queueOfWaypoints.Enqueue(StartWaypoint);
        while (queueOfWaypoints.Count > 0 && isRunning)
        {
            searchCenter = queueOfWaypoints.Dequeue();
            searchCenter.isExplored = true;
            CheckNeigbours();
        }
        AddWaypointsToThePath();
    }

    Vector2Int[] RandomizeDirectionOfSearch()
    {
        int random = UnityEngine.Random.Range(1, 4);
        switch (random)
        {   case 1:
                directions = new Vector2Int[4] { Vector2Int.right, Vector2Int.down, Vector2Int.left, Vector2Int.up };
                break;
            case 2:
                directions = new Vector2Int[4] { Vector2Int.down, Vector2Int.left, Vector2Int.up, Vector2Int.right };
                break;
            case 3:
                directions = new Vector2Int[4] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
                break;
            case 4:
                directions = new Vector2Int[4] {Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };
                break;
            default:
                directions = new Vector2Int[4] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
            break;
        }

        return directions;
    }

    private void AddWaypointsToThePath()
    {
        Path.Add(EndWaypoint);
        Waypoint2 WaypointToAdd = EndWaypoint.foundByWaypoint;
        while (WaypointToAdd != StartWaypoint)
        {
            Path.Add(WaypointToAdd);
            WaypointToAdd.SetTopColor(ColorPath);
            WaypointToAdd = WaypointToAdd.foundByWaypoint;
        }
        Path.Add(StartWaypoint);
        Path.Reverse();
    }

    public List<Waypoint2> GetPath() { return Path; }

    private void CheckNeigbours()
    {
        foreach (Vector2Int direction in RandomizeDirectionOfSearch())
        {
            Vector2Int explorationcoordinates = searchCenter.GetGridPos() + direction;
            print("exploring " + explorationcoordinates);
            try
            {
                Waypoint2 neighbour = grid[explorationcoordinates];

                neighbour.SetTopColor(ColorExploration);
                if (neighbour.isExplored == true || queueOfWaypoints.Contains(neighbour))
                {
                    //do nothing
                }
                else
                {
                    queueOfWaypoints.Enqueue(neighbour);
                    neighbour.foundByWaypoint = searchCenter;
                }
                CheckIsEnd(neighbour);
            }
            catch
            {
                //do nothing
            }
        }
    }

    void CheckIsEnd(Waypoint2 checkedWaypoint)
    {
        if (checkedWaypoint == EndWaypoint)
        {
            isRunning = false;
            queueOfWaypoints.Clear();
        }
    }

    void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint2>();

        foreach (Waypoint2 waypoint in waypoints)
        {
            var GridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(GridPos))
            {
                Debug.Log("Overlaping block " + waypoint);
            }
            else
            {
                grid.Add(GridPos, waypoint);
                waypoint.SetTopColor(Color.black);
            }         
        }
        print("Dictionary ma elementów: " + grid.Count);
    }

    private void ColorStartAndEnd()
    {
        StartWaypoint.SetTopColor(ColorStart);
        EndWaypoint.SetTopColor(ColorEnd);
    }
}
