﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class CubeEditor : MonoBehaviour
{
    [SerializeField] [Range(1,10)]
    private int gridSize = 10;

    TextMesh textMesh;
    private void Start()
    {
        
    }

    void Update()
    {
        textMesh = GetComponentInChildren<TextMesh>();

        Vector3 snapPosition;
        snapPosition.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPosition.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;

        transform.position = new Vector3(snapPosition.x, 0f, snapPosition.z);
        string labelText = snapPosition.x / gridSize + "," + snapPosition.z / gridSize;

        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
