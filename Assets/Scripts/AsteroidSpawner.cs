using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    // timer support
    Timer spawnTimer;
    const float SpawnTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = SpawnTime;
        spawnTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer.Finished)
        {
            SpawnAsteroid();
            spawnTimer.Run();
        }
    }

    // Spawns an asteroid
    void SpawnAsteroid()
    {
        // populate asteroids array
        List<GameObject> asteroids = new List<GameObject>();
        asteroids.Add(Resources.Load<GameObject>(@"Prefabs/AsteroidSmall"));
        asteroids.Add(Resources.Load<GameObject>(@"Prefabs/AsteroidMedium"));
        asteroids.Add(Resources.Load<GameObject>(@"Prefabs/AsteroidBig"));

        // spawn random asteroid
        Vector3 spawnLocation = new Vector3(0, 0, -Camera.main.transform.position.z);
        spawnLocation = Camera.main.ScreenToWorldPoint(spawnLocation);
        Instantiate(asteroids[((int)Random.Range(0, 3))], 
            spawnLocation, Quaternion.identity);
    }
}
