using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint2))]

public class CubeEditor : MonoBehaviour
{
    TextMesh textMesh;
    Waypoint2 waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint2>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        transform.position = new Vector3(waypoint.GetGridPos().x, 0f, waypoint.GetGridPos().y);
    }

    private void UpdateLabel()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        string labelText = waypoint.GetGridPos().x / waypoint.GetGridSize() + "," + waypoint.GetGridPos().y / waypoint.GetGridSize();
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
