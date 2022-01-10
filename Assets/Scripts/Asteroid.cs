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

    #endregion

    #region Methods

    /// <summary>
    /// Awake is called on instantiated
    /// </summary>
    void Awake()
    {
        // initialized for efficiency
        rigidbody2d = GetComponent<Rigidbody2D>();
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

    #endregion
}
