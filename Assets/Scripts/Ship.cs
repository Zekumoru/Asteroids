using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    #region Fields

    // movement related  fields
    const float RotateDegreesPerSecond = 270f;
    const float ShipSpeedThreshold = 7.2f;
    const float ShipAcceleration = 1.6f;
    const float RotationThreshold = 5;
    const float BrakeAcceleration = 5.6f;

    // input related
    const float FireSpeedInSeconds = 0.3f;

    // timer support
    Timer fireTimer;

    // cached for performance
    GameObject prefabExplosion;
    Rigidbody2D rigidBody2d;

    #endregion

    #region Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        prefabExplosion = Resources.Load<GameObject>(@"Prefabs/Explosion");
        rigidBody2d = GetComponent<Rigidbody2D>();

        // create and start fire timer
        fireTimer = gameObject.AddComponent<Timer>();
        fireTimer.Duration = FireSpeedInSeconds;
        fireTimer.Run();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        InputRotation();
        InputFire();
    }

    // handle rotation on user's input
    void InputRotation()
    {
        Vector3 rotation = transform.eulerAngles;

        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            rotation -= Vector3.forward * horizontalInput *
                RotateDegreesPerSecond * Time.deltaTime;
        }

        transform.eulerAngles = rotation;
    }

    // handle firing bullets
    void InputFire()
    {
        // check if timer is finished
        if (fireTimer.Finished && Input.GetAxis("Fire") > 0)
        {
            // instantiate bullet then fire
            Instantiate(Resources.Load<GameObject>(@"Prefabs/Bullet"),
                gameObject.transform.position, 
                gameObject.transform.rotation);

            // restart timer
            fireTimer.Run();
        }
    }

    /// <summary>
    /// Called 50 times per second.
    /// </summary>
    void FixedUpdate()
    {
        // handle thrust on user's input
        Vector3 rotation = transform.eulerAngles;
        Vector2 velocity = rigidBody2d.velocity;
        Vector2 direction = new Vector2(
                -Mathf.Sin(rotation.z * Mathf.PI / 180),
                Mathf.Cos(rotation.z * Mathf.PI / 180));
        direction *= ShipAcceleration;
        float thrust = Input.GetAxis("Thrust");

        if (thrust > 0)
        {
            // apply force
            if (velocity.magnitude < 2.6f) rigidBody2d.AddForce(direction * 10, ForceMode2D.Force);
            else rigidBody2d.AddForce(direction, ForceMode2D.Force);

            // constraint velocity
            velocity = rigidBody2d.velocity;
            if (velocity.x < -ShipSpeedThreshold || velocity.x > ShipSpeedThreshold)
            {
                velocity.x = ShipSpeedThreshold * Mathf.Sign(velocity.x) - 0.01f;
            }
            if (velocity.y < -ShipSpeedThreshold || velocity.y > ShipSpeedThreshold)
            {
                velocity.y = ShipSpeedThreshold * Mathf.Sign(velocity.y) - 0.01f;
            }

            // slow down on turn
            if ((direction.x > 0 && velocity.x < 0) || (direction.x < 0 && velocity.x > 0))
            {
                velocity.x -= velocity.x * Time.deltaTime * BrakeAcceleration;
            }
            if ((direction.y > 0 && velocity.y < 0) || (direction.y < 0 && velocity.y > 0))
            {
                velocity.y -= velocity.y * Time.deltaTime * BrakeAcceleration;
            }

            // slow down based on angle
            float currentRotation = -Mathf.Atan2(velocity.x, velocity.y) * 180 / Mathf.PI;
            if (currentRotation < 0) currentRotation += 360;
            if (!(currentRotation > rotation.z - RotationThreshold && currentRotation < rotation.z + RotationThreshold))
            {
                velocity.x -= velocity.x * Time.deltaTime * BrakeAcceleration;
                velocity.y -= velocity.y * Time.deltaTime * BrakeAcceleration;
            }
        }
        else
        {
            velocity.x -= velocity.x * Time.deltaTime * BrakeAcceleration;
            velocity.y -= velocity.y * Time.deltaTime * BrakeAcceleration;
        }

        rigidBody2d.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AsteroidSmall")
            || collision.gameObject.CompareTag("AsteroidMedium")
            || collision.gameObject.CompareTag("AsteroidBig"))
        {
            collision.gameObject.GetComponent<Asteroid>().Explode();
            Destroy(gameObject);
            Instantiate(prefabExplosion, transform.position, Quaternion.identity);
        }
    }

    #endregion
}
