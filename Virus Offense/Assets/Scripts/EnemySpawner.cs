using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject routCounter;

    [Header("Enemies")]
    [SerializeField] List<float> enemySpawnTimes;
    [SerializeField] List<GameObject> enemySpawnPrefab;
    
    float spawnTimer = 0;
    GameObject player;

    // Find player tagged object while adding enemies to rout objective
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        routCounter.GetComponent<RoutCounter>().UpdateSpawnerCount(enemySpawnPrefab.Count);
    }

    // Update is called once per frame
    void Update()
    {
        // Unless enemySpawnTimes or enemySpawnTypes lists are empty, keep counting up the timer
        if (!(enemySpawnTimes.Count == 0 || enemySpawnPrefab.Count == 0))
        {
            spawnTimer += Time.deltaTime;

            // If timer reaches a specified spawn time in enemySpawnTimes, spawn that enemy as child and remove the first element in both lists
            if (spawnTimer >= enemySpawnTimes[0])
            {
                GameObject newEnemy = Instantiate(enemySpawnPrefab[0], transform) as GameObject;
                newEnemy.GetComponent<EnemyMovement>().GainTarget(player);

                enemySpawnTimes.RemoveAt(0);
                enemySpawnPrefab.RemoveAt(0);

                routCounter.GetComponent<RoutCounter>().UpdateSpawnerCount(-1); // Remove enemy from Rout counter, as it spawns in world
            }
        }
    }
}
