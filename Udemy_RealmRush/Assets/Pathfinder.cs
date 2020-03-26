using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    GameControler gameControler;
    Dictionary<Vector2Int, Waypoint2> grid = new Dictionary<Vector2Int, Waypoint2>();
    Dictionary<Vector2Int, Waypoint2> path = new Dictionary<Vector2Int, Waypoint2>();

    GameObject myTop;
    [SerializeField] bool isRunning = true;
    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
    Queue<Waypoint2> queueOfWaypoints = new Queue<Waypoint2>();
    List<Waypoint2> Path = new List<Waypoint2>();
    Waypoint2 searchCenter;
    private bool pathReady = false;
    

    void Start()
    {
        gameControler = FindObjectOfType<GameControler>();
        LoadBlocks();
        Pathfind();
        //ColorStartAndEnd();
        StartEnemyMovement();
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

    void Pathfind()
    {
        ClearExploredMarks();
        queueOfWaypoints.Enqueue(gameControler.StartWaypoint);
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
        Path.Add(gameControler.EndWaypoint);
        Waypoint2 WaypointToAdd = gameControler.EndWaypoint.foundByWaypoint;
        while (WaypointToAdd != gameControler.StartWaypoint)
        {
            Path.Add(WaypointToAdd);
            WaypointToAdd.SetTopColor(gameControler.ColorPath);
            WaypointToAdd = WaypointToAdd.foundByWaypoint;
        }
        Path.Add(gameControler.StartWaypoint);
        Path.Reverse();
        pathReady = true;
    }

    public List<Waypoint2> GetPath() { return Path; }
    public Waypoint2 GetStartWaypoint() { return gameControler.StartWaypoint; }

    private void CheckNeigbours()
    {
        foreach (Vector2Int direction in RandomizeDirectionOfSearch())
        {
            Vector2Int explorationcoordinates = searchCenter.GetGridPos() + direction;
            print("exploring " + explorationcoordinates);
            try
            {
                Waypoint2 neighbour = grid[explorationcoordinates];

                //neighbour.SetTopColor(gameControler.ColorExploration);
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

    private void ClearExploredMarks()
    {
        var waypoints = FindObjectsOfType<Waypoint2>();
        foreach (Waypoint2 waypoint in waypoints)
        { waypoint.isExplored = false; }
    }

    void CheckIsEnd(Waypoint2 checkedWaypoint)
    {
        if (checkedWaypoint == gameControler.EndWaypoint)
        {
            isRunning = false;
            queueOfWaypoints.Clear();
        }
    }

    private void ColorStartAndEnd()
    {
        gameControler.StartWaypoint.SetTopColor(gameControler.ColorStart);
        gameControler.EndWaypoint.SetTopColor(gameControler.ColorEnd);
    }

    public bool GetPathReady()
    {
        return pathReady;
    }

    public void StartEnemyMovement()
    {
        gameObject.GetComponent<EnemyMovement>().StartMovement();
    }
}
