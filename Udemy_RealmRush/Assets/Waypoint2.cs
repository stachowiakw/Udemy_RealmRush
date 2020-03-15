using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint2 : MonoBehaviour
{
    const int gridSize = 10;

    // Start is called before the first frame update
    public int GetGridSize()
    {
        return gridSize;
    }
        
    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize) * gridSize,
            Mathf.RoundToInt(transform.position.z / gridSize) * gridSize
        );
    }
}
