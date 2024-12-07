using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    public GameObject Enemy;
    public bool spawnAtRandomPoints = true;
    public int maxSpawnDelay = 5;
    public int minSpawnDelay = 2;
    public int maxEnemies = 20;
    private int currentSpawnpoint = 0;
    private float spawnDelay = 0f;
    private float time = 0f;
    private bool canSpawn = true;
    private System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        spawnDelay = rand.Next(minSpawnDelay,maxSpawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Count() >= maxEnemies){
            canSpawn = false;
        } else {
            canSpawn = true;
        }
        if (Enemy != null && spawnPoints.Count != 0 && canSpawn) {
            if (time >= spawnDelay && spawnAtRandomPoints) {
                currentSpawnpoint = rand.Next(0,spawnPoints.Count);
                Instantiate(Enemy, spawnPoints[currentSpawnpoint]);
                time = 0;
                spawnDelay = rand.Next(minSpawnDelay,maxSpawnDelay);
            } else if (time >= spawnDelay && !spawnAtRandomPoints) {
                Instantiate(Enemy, spawnPoints[currentSpawnpoint]);
                time = 0;
                spawnDelay = rand.Next(minSpawnDelay,maxSpawnDelay);
                currentSpawnpoint++;
                if (currentSpawnpoint >= spawnPoints.Count) {
                    currentSpawnpoint = 0;
                }
            }
        }
        time += Time.deltaTime;
    }
}
