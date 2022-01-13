using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    #region Fields

    // Constants
    const float MinMagnitude = 0.6f;
    const float MaxMagnitude = 1.6f;

    // saved for efficiency
    Rigidbody2D rigidbody2d;

    // asteroid prefabs
    GameObject asteroidSmall;
    GameObject asteroidMedium;

    // explosion animation prefabs
    GameObject asteroidSmallAnim;
    GameObject asteroidMediumAnim;
    GameObject asteroidBigAnim;

    #endregion

    #region Methods

    /// <summary>
    /// Awake is called on instantiated
    /// </summary>
    void Awake()
    {
        // initialized for efficiency
        rigidbody2d = GetComponent<Rigidbody2D>();
        asteroidSmall =
            Resources.Load<GameObject>(@"Prefabs/AsteroidSmall");
        asteroidMedium =
            Resources.Load<GameObject>(@"Prefabs/AsteroidMedium");
        asteroidSmallAnim =
            Resources.Load<GameObject>(@"Prefabs/AsteroidSmallAnim");
        asteroidMediumAnim = 
            Resources.Load<GameObject>(@"Prefabs/AsteroidMediumAnim");
        asteroidBigAnim =
            Resources.Load<GameObject>(@"Prefabs/AsteroidBigAnim");

        // initialize direction
        MoveInRandomDirection();
    }

    /// <summary>
    /// Check if collided with a bullet
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Explode();
        }
    }

    /// <summary>
    /// Generates random direction and magnitude
    /// to get the asteroid moving.
    /// </summary>
    public void MoveInRandomDirection()
    {
        // generate random direction
        float angle = Random.Range(0, 2 * Mathf.PI);
        Vector2 direction = new Vector2(
            Mathf.Cos(angle), Mathf.Sin(angle));

        // move the asteroid
        MoveInDirection(direction);
    }

    /// <summary>
    /// Generates random magnitude then moves
    /// the asteroid to specified direction.
    /// </summary>
    public void MoveInDirection(Vector2 direction)
    {
        // calculate random magnitude
        float magnitude = Random.Range(MinMagnitude, MaxMagnitude);

        // apply gravity
        rigidbody2d.velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(
            direction * magnitude, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Destroys asteroid and plays the exploding animation
    /// </summary>
    public void Explode()
    {
        Vector3 position = gameObject.transform.position;
        Quaternion rotation = gameObject.transform.rotation;
        string tag = gameObject.tag;
        Destroy(gameObject);
        if (tag.Equals("AsteroidSmall"))
        {
            Instantiate(asteroidSmallAnim, position, rotation);
        }
        else if (tag.Equals("AsteroidMedium"))
        {
            Instantiate(asteroidMediumAnim, position, rotation);
        }
        else if (tag.Equals("AsteroidBig"))
        {
            Instantiate(asteroidBigAnim, position, rotation);
        }
        Scatter();
    }

    /// <summary>
    /// Spawns smaller asteroids depending on the size
    /// of the asteroid
    /// 
    /// If the asteroid is small, do nothing.
    /// If the asteroid is medium, 
    ///     spawn 1 or 2 small asteroids (30% chance)
    /// If the asteroid is big,
    ///     spawn 1 medium and 1 or 
    ///     2 small asteroids (30% chance)
    /// </summary>
    private void Scatter()
    {
        Vector3 position = gameObject.transform.position;
        string tag = gameObject.tag;

        // check asteroid size
        if (tag.Equals("AsteroidMedium"))
        {
            // instantiate asteroids
            Instantiate(asteroidSmall, position, Quaternion.identity);
            if (Random.Range(0f, 1f) < 0.3f)
            {
                Instantiate(asteroidSmall, position, Quaternion.identity);
            }
        }
        else if (tag.Equals("AsteroidBig"))
        {
            // instantiate asteroids
            Instantiate(asteroidMedium, position, Quaternion.identity);
            Instantiate(asteroidSmall, position, Quaternion.identity);
            if (Random.Range(0f, 1f) < 0.3f)
            {
                Instantiate(asteroidSmall, position, Quaternion.identity);
            }
        }
    }

    #endregion
}
