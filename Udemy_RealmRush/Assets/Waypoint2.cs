using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint2 : MonoBehaviour
{
    const int gridSize = 10;
    public bool isExplored = false;
    public Waypoint2 foundByWaypoint;

    // Start is called before the first frame update
    public int GetGridSize()
    {
        return gridSize;
    }
        
    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt((transform.position.x / gridSize)),
            Mathf.RoundToInt((transform.position.z / gridSize))
        );
    }

    public void SetTopColor(Color newColor)
    {
        MeshRenderer topMR = transform.Find("root").Find("Top").GetComponent<MeshRenderer>();
        topMR.material.color = newColor;
    }
}
