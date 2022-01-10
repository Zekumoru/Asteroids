using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Fields
    const float MinMagnitude = 0.6f;
    const float MaxMagnitude = 1.6f;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        Move();
    }

    /// <summary>
    /// Generates random direction and magnitude
    /// to get the asteroid moving.
    /// </summary>
    void Move()
    {
        // calculate random magnitude
        float magnitude = Random.Range(MinMagnitude, MaxMagnitude);

        // generate random direction
        float angle = Random.Range(0, 2 * Mathf.PI);
        Vector2 direction = new Vector2(
            Mathf.Cos(angle), Mathf.Sin(angle));

        // apply gravity
        GetComponent<Rigidbody2D>().AddForce(
            direction * magnitude, ForceMode2D.Impulse);
    }
}
