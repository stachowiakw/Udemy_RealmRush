using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    List<Waypoint2> Path;
    Pathfinder pathfinder;
    
    // Start is called before the first frame update
    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        Path = pathfinder.GetPath();
        StartCoroutine(FollowPath());
        print("But I can still do other stuff :)");
    }

   IEnumerator FollowPath()
    {
        print("Starting patrol...");
        foreach (Waypoint2 waypoint in Path)
        {
            print("Visiting: "+ waypoint.name);
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
        print("Ending patrol.");
    }
}
