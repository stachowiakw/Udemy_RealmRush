using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    GameControler gameControler;
    [SerializeField] int Health = 100;
    [SerializeField] int damageByTower = 1;
    [SerializeField] GameObject ParticleObjectToSpawnOnDeath;
    [SerializeField] GameObject ParticleObjectToSpawnOnSelfDestruct;
    // Start is called before the first frame update
    void Start()
    {
        gameControler = FindObjectOfType<GameControler>();
    }

    void OnParticleCollision(GameObject other)
    {
        print("DOSTAŁEM");
        Health = Health - damageByTower;
        if (Health <= 0) { DestroyEnemy(); }
    }

    private void DestroyEnemy()
    {
        gameControler.enemies.Remove(gameObject);
        var DestroyParticleSystem = Instantiate(ParticleObjectToSpawnOnDeath, GameObject.Find("Enemy_A").transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(DestroyParticleSystem, DestroyParticleSystem.GetComponent<ParticleSystem>().main.duration);
    }

    public void SelfDestuct()
    {
        gameControler.enemies.Remove(gameObject);
        var DestroyParticleSystem = Instantiate(ParticleObjectToSpawnOnSelfDestruct, GameObject.Find("Enemy_A").transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(DestroyParticleSystem, DestroyParticleSystem.GetComponent<ParticleSystem>().main.duration);
    }
}
