using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] bool targetLocked = false;
    ParticleSystem cannon;
    Dictionary<Vector2Int, Waypoint2> gridSurroundingTower = new Dictionary<Vector2Int, Waypoint2>();

    private void Start()
    {
        cannon = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (targetLocked) { LookAtEnemy();}
    }

    void LookAtEnemy()
    {
      objectToPan.LookAt(targetEnemy);
    }

    void SearchForTarget()
    {
        var enemies = FindObjectsOfType<EnemyBehaviour>();
        targetEnemy = enemies[0].transform.Find("Enemy_A").transform;
        if (targetEnemy) { targetLocked = true; }
        else { targetLocked = false; }
    }

    private void OnTriggerEnter(Collider enemy)
    {
        LockOnTarget(enemy.gameObject);
    }

    private void OnTriggerExit(Collider exitingEnemy)
    {
        if (exitingEnemy.transform == targetEnemy)
            UnlockTarget();
    }

    private void OnTriggerStay(Collider enemy)
    {
        LockOnTarget(enemy.gameObject);
    }

    private void UnlockTarget()
    {
        Shooting(false);
        targetEnemy = null;
        targetLocked = false;
    }

    private void LockOnTarget(GameObject enemy)
    {
       if (!targetLocked)
        {
            targetEnemy = enemy.transform;
            targetLocked = true;
            Shooting(true);
        }
    }
    private void Shooting(bool isShooting)
    {
        var cannonShooting = cannon.emission;
        cannonShooting.enabled = isShooting;
    }
}
