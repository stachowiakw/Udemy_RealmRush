using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint2> grid = new Dictionary<Vector2Int, Waypoint2>();
    GameObject myTop;
    [SerializeField] Waypoint2 StartWaypoint, EndWaypoint;
    [SerializeField] Color ColorStart, ColorEnd;

    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
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
