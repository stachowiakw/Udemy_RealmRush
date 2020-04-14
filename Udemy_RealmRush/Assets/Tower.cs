using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform objectToPan;
    [SerializeField] private Transform targetEnemy;
    [SerializeField] private float towerRange = 10f;
    [SerializeField] private bool targetInRange = false;
    private ParticleSystem cannon;
    private Dictionary<Vector2Int, Waypoint2> gridSurroundingTower = new Dictionary<Vector2Int, Waypoint2>();
    public Waypoint2 sebaWaypoint;

    private void Start()
    {
        cannon = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        LookAtEnemy();
        if (targetInRange == true)       { Shooting(true); }
                             else        { Shooting(false);
                                           SearchForTarget(); }
    }

    void LookAtEnemy()
    {
        if(!targetEnemy) { return; }
        objectToPan.LookAt(targetEnemy);
        IsTargetInRange(targetEnemy);
    }

    void SearchForTarget()
    {
        var enemies = FindObjectsOfType<EnemyBehaviour>();
        if (enemies.Length == 0) { return; }

        Transform closestEnemy = enemies[0].transform.Find("Enemy_A").transform;

        foreach (EnemyBehaviour enemyToCheck in enemies)
        {
            closestEnemy = GetClosest(closestEnemy, enemyToCheck.transform);
        }

        targetEnemy = closestEnemy;
        IsTargetInRange(targetEnemy);
    }

    private void IsTargetInRange(Transform targetEnemy)
    {
        if (Vector3.Distance(gameObject.transform.position, targetEnemy.position) <= towerRange) { targetInRange = true; }
        else { targetInRange = false; };
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(gameObject.transform.position, transformA.position);
        var distToB = Vector3.Distance(gameObject.transform.position, transformB.position);

        if (distToA < distToB) { return transformA; }
        else { return transformB; }
    }

    private void Shooting(bool isShooting)
    {
        var cannonShooting = cannon.emission;
        cannonShooting.enabled = isShooting;
    }
}
