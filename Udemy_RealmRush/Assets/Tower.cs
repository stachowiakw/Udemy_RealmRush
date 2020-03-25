using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    Dictionary<Vector2Int, Waypoint2> gridSurroundingTower = new Dictionary<Vector2Int, Waypoint2>();
    GameControler gameControler;
    bool targetLocked;

    private void Start()
    {
        gameControler = FindObjectOfType<GameControler>();
        
    }

    void Update()
    {
        lookAtEnemy();  
        if(!targetLocked) { SearchForTarget(); }
    }

    void lookAtEnemy()
    {
        if (targetEnemy)
        { objectToPan.LookAt(targetEnemy); }
    }

    void SearchForTarget()
    {
        var enemies = FindObjectsOfType<EnemyBehaviour>();
        targetEnemy = enemies[0].transform.Find("Enemy_A").transform;
        if (targetEnemy) { targetLocked = true; }
        else { targetLocked = false; }
    }
}
