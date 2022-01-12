using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Fields

    // constants
    const float SelfDestructAfterSeconds = 0.8f;
    const float Magnitude = 12.4f;

    // timer support
    Timer deathTimer;

    #endregion

    #region Methods

    // Gets called on the next frame
    void Start()
    {
        // get the bullet moving
        Vector2 direction = new Vector2(
            -Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad),
            Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad));
        GetComponent<Rigidbody2D>().AddForce(
            direction * Magnitude, ForceMode2D.Impulse);

        // create and run death timer
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = SelfDestructAfterSeconds;
        deathTimer.Run();
    }

    // Gets called every frame
    void Update()
    {
        if (deathTimer.Finished)
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
