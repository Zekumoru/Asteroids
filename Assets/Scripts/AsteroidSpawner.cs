using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    // constants
    const float MinDirectionRange = -0.3f;
    const float MaxDirectionRange = 0.3f;

    // timer support
    Timer spawnTimer;
    const float SpawnTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Spawn4AsteroidsOnScreenMiddleEdges();

        // get timer ready then run
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = SpawnTime;
        spawnTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        #region Periodic Spawn (Not used)
        //if (spawnTimer.Finished)
        //{
        //    Vector3 spawnLocation = new Vector3(0, 0, -Camera.main.transform.position.z);
        //    spawnLocation = Camera.main.ScreenToWorldPoint(spawnLocation);
        //    SpawnAsteroid(spawnLocation);
        //    spawnTimer.Run();
        //}
        #endregion
    }

    // Spawns 4 asteroids in the middle of the screen edges
    void Spawn4AsteroidsOnScreenMiddleEdges()
    {
        Vector2 direction;
        Vector3 location;

        // spawn in the middle top edge then move to center
        location = new Vector3(ScreenUtils.ScreenMiddleWidth,
            ScreenUtils.ScreenTop, ScreenUtils.ScreenZ);
        direction = new Vector2(Random.Range(MinDirectionRange, MaxDirectionRange), -1);
        SpawnAsteroid(location).
            GetComponent<Asteroid>().MoveInDirection(direction);

        // spawn in the middle right edge then move to center
        location = new Vector3(ScreenUtils.ScreenRight,
            ScreenUtils.ScreenMiddleHeight, ScreenUtils.ScreenZ);
        direction = new Vector2(-1, Random.Range(MinDirectionRange, MaxDirectionRange));
        SpawnAsteroid(location).
            GetComponent<Asteroid>().MoveInDirection(direction);

        // spawn in the middle bottom edge then move to center
        location = new Vector3(ScreenUtils.ScreenMiddleWidth,
            ScreenUtils.ScreenBottom, ScreenUtils.ScreenZ);
        direction = new Vector2(Random.Range(MinDirectionRange, MaxDirectionRange), 1);
        SpawnAsteroid(location).
            GetComponent<Asteroid>().MoveInDirection(direction);

        // spawn in the middle left edge then move to center
        location = new Vector3(ScreenUtils.ScreenLeft,
            ScreenUtils.ScreenMiddleHeight, ScreenUtils.ScreenZ);
        direction = new Vector2(1, Random.Range(MinDirectionRange, MaxDirectionRange));
        SpawnAsteroid(location).
            GetComponent<Asteroid>().MoveInDirection(direction);

    }

    // Spawns an asteroid
    GameObject SpawnAsteroid(Vector3 spawnLocation)
    {
        // populate asteroids array
        List<GameObject> asteroids = new List<GameObject>();
        asteroids.Add(Resources.Load<GameObject>(@"Prefabs/AsteroidSmall"));
        asteroids.Add(Resources.Load<GameObject>(@"Prefabs/AsteroidMedium"));
        asteroids.Add(Resources.Load<GameObject>(@"Prefabs/AsteroidBig"));

        // spawn random asteroid then move it
        return Instantiate(asteroids[((int)Random.Range(0, 3))],
            spawnLocation, Quaternion.identity) as GameObject;
    }
}
