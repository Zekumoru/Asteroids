using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    const int SpawnNumberOfAsteroids = 30;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAsteroids(SpawnNumberOfAsteroids);
    }

    void SpawnAsteroids(int numberOfAsteroids)
    {
        Vector3 location = ScreenUtils.ScreenCenter;

        // saved for efficiency
        float width = ScreenUtils.ScreenRight - ScreenUtils.ScreenLeft;
        float height = ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom;

        // generate asteroids
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            // pick edge
            int randomEdge = Random.Range(0, 4);
            if (randomEdge == 0)
            {
                // top edge
                location.x = Random.Range(ScreenUtils.ScreenLeft,
                    ScreenUtils.ScreenRight);
                location.y = ScreenUtils.ScreenTop;
            }
            else if (randomEdge == 1)
            {
                // right edge
                location.x = ScreenUtils.ScreenRight;
                location.y = Random.Range(ScreenUtils.ScreenBottom,
                    ScreenUtils.ScreenTop);
            }
            else if (randomEdge == 2)
            {
                // bottom edge
                location.x = Random.Range(ScreenUtils.ScreenLeft,
                    ScreenUtils.ScreenRight);
                location.y = ScreenUtils.ScreenBottom;
            }
            else
            {
                // left edge
                location.x = ScreenUtils.ScreenLeft;
                location.y = Random.Range(ScreenUtils.ScreenBottom,
                    ScreenUtils.ScreenTop);
            }

            SpawnAsteroid(location);
        }
    }

    // Spawns 4 asteroids in the middle of the screen edges
    void Spawn4AsteroidsOnScreenMiddleEdges()
    {
        const float MinDirectionRange = -0.3f;
        const float MaxDirectionRange = 0.3f;
        Vector2 direction;
        Vector3 location;

        // spawn in the middle top edge then move to center
        location = ScreenUtils.ScreenCenter;
        location.y = ScreenUtils.ScreenTop;
        direction = new Vector2(Random.Range(MinDirectionRange, MaxDirectionRange), -1);
        SpawnAsteroid(location).
            GetComponent<Asteroid>().MoveInDirection(direction);

        // spawn in the middle right edge then move to center
        location = ScreenUtils.ScreenCenter;
        location.x = ScreenUtils.ScreenRight;
        direction = new Vector2(-1, Random.Range(MinDirectionRange, MaxDirectionRange));
        SpawnAsteroid(location).
            GetComponent<Asteroid>().MoveInDirection(direction);

        // spawn in the middle bottom edge then move to center
        location = ScreenUtils.ScreenCenter;
        location.y = ScreenUtils.ScreenBottom;
        direction = new Vector2(Random.Range(MinDirectionRange, MaxDirectionRange), 1);
        SpawnAsteroid(location).
            GetComponent<Asteroid>().MoveInDirection(direction);

        // spawn in the middle left edge then move to center
        location = ScreenUtils.ScreenCenter;
        location.x = ScreenUtils.ScreenLeft;
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
