using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    [SerializeField] public Waypoint2 StartWaypoint, EndWaypoint;
    [SerializeField] public Color ColorStart, ColorEnd, ColorExploration, ColorPath;

    public List<GameObject> enemies = new List<GameObject>();
    [SerializeField]
    private GameObject EnemyType1;
    private Pathfinder pathfinder;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        SpawnEnemy();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { SpawnEnemy(); }
    }

    void SpawnEnemy()
    {
        Vector3 SpawnPoint = new Vector3();
        SpawnPoint = StartWaypoint.transform.position;
        var SpawnedEnemy = Instantiate(EnemyType1, SpawnPoint, Quaternion.identity);
        enemies.Add(SpawnedEnemy);
    }
}
