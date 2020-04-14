using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    [SerializeField] public Waypoint2 StartWaypoint, EndWaypoint;
    [SerializeField] public Color ColorStart, ColorEnd, ColorExploration, ColorPath;
    [SerializeField] private GameObject objectToSpawn;
    public List<GameObject> enemies = new List<GameObject>();
    Queue<GameObject> towerQueue = new Queue<GameObject>();
    [SerializeField] int towersLimit = 3;

    public GameObject GetObjectToSpawn() { return objectToSpawn; }


    public void ManageTowers(Waypoint2 baseWaypoint)
    {
        print("########################"+towerQueue.Count);
        if (FindObjectsOfType<Tower>().Length >= towersLimit)
        {
            MoveTower(baseWaypoint);
        }
        else
        {
            InstantiateTower(baseWaypoint);
        }
        print("########################" + towerQueue.Count);
    }

    private void InstantiateTower(Waypoint2 baseWaypoint)
    {
        var newTower = Instantiate(objectToSpawn, baseWaypoint.transform.position, Quaternion.identity);
        newTower.GetComponent<Tower>().sebaWaypoint = baseWaypoint;
        towerQueue.Enqueue(newTower);
        baseWaypoint.waypointWithObject = true;
    }

    private void MoveTower(Waypoint2 baseWaypoint)
    {
        var oldTower = towerQueue.Dequeue();
        oldTower.GetComponent<Tower>().sebaWaypoint.waypointWithObject = false;
        oldTower.GetComponent<Tower>().sebaWaypoint = baseWaypoint;
        oldTower.GetComponent<Tower>().sebaWaypoint.waypointWithObject = true;
        oldTower.transform.position = baseWaypoint.transform.position;
        towerQueue.Enqueue(oldTower);
    }
}


