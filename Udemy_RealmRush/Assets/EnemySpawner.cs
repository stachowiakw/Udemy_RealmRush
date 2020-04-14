using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameControler gameControler;
    [SerializeField]
    float secondsBetweenSpawns = 3f;
    [SerializeField]
    private GameObject EnemyType1;
    // Start is called before the first frame update
    void Start()
    {
        gameControler = FindObjectOfType<GameControler>();
        StartCoroutine(RepeatedSpawnEnemy());
    }

    IEnumerator RepeatedSpawnEnemy()
    {
        while (true)
        {
            var SpawnedEnemy = Instantiate(EnemyType1, gameObject.transform );
            gameControler.enemies.Add(SpawnedEnemy);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
     }
}
