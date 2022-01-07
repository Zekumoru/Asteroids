using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    // movement related  fields
    const float RotateDegreesPerSecond = 270f;
    const float ShipSpeedThreshold = 7.2f;
    const float ShipAcceleration = 1.6f;
    const float RotationThreshold = 5;
    const float BrakeAcceleration = 5.6f;

    // cached for performance
    Rigidbody2D rigidBody2d;
    float colliderScreenOutWidth;
    float colliderScreenOutHeight;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        colliderScreenOutWidth = collider.size.x * 0.75f;
        colliderScreenOutHeight = collider.size.y * 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessPlayerInput();
        OnScreenEdgeEncounter();
    }

    /// <summary>
    /// Processes player's keyboard input to move the ship
    /// </summary>
    void ProcessPlayerInput()
    {
        Vector3 rotation = transform.eulerAngles;

        // horizontal movement (left and right)
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            rotation -= Vector3.forward * horizontalInput * 
                RotateDegreesPerSecond * Time.deltaTime;
        }

        // thrust the ship
        Vector2 velocity = rigidBody2d.velocity;
        Vector2 direction = new Vector2(
                -Mathf.Sin(rotation.z * Mathf.PI / 180),
                Mathf.Cos(rotation.z * Mathf.PI / 180));
        direction *= ShipAcceleration;
        float thrust = Input.GetAxis("Thrust");
        if (thrust > 0)
        {
            // apply force
            rigidBody2d.AddForce(direction, ForceMode2D.Force);

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
        transform.eulerAngles = rotation;
    }

    /// <summary>
    /// If the ship leaves the screen, 
    /// teleport it to the other edge
    /// </summary>
    void OnScreenEdgeEncounter()
    {
        Vector3 position = transform.position;
        if (position.x + colliderScreenOutWidth < ScreenUtils.ScreenLeft)
        {
            position.x = ScreenUtils.ScreenRight + colliderScreenOutWidth;
        }
        else if (position.x - colliderScreenOutWidth > ScreenUtils.ScreenRight)
        {
            position.x = ScreenUtils.ScreenLeft - colliderScreenOutWidth; 
        }
        if (position.y + colliderScreenOutHeight < ScreenUtils.ScreenBottom)
        {
            position.y = ScreenUtils.ScreenTop + colliderScreenOutHeight;
        }
        if (position.y - colliderScreenOutHeight > ScreenUtils.ScreenTop)
        {
            position.y = ScreenUtils.ScreenBottom - colliderScreenOutHeight;
        }
        transform.position = position;
    }
}
