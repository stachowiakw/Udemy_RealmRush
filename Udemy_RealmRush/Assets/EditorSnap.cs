using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorSnap : MonoBehaviour
{
    [SerializeField] [Range(1,10)]
    private int gridSize = 10;

        void Update()
        {
        Vector3 snapPosition;
        snapPosition.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPosition.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;

        transform.position = new Vector3(snapPosition.x, 0f, snapPosition.z);
        }
}
