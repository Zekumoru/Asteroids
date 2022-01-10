using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    // cached for performance
    float colliderWidth;
    float colliderHeight;

    // Start is called before the first frame update
    void Start()
    {
        InitColliderSize();
    }

    /// <summary>
    /// Called when the game object became invisible to the camera
    /// </summary>
    void OnBecameInvisible()
    {
        Vector3 position = transform.position;
        if (position.x - colliderWidth < ScreenUtils.ScreenLeft ||
            position.x + colliderWidth > ScreenUtils.ScreenRight)
        {
            position.x *= -1;
        }
        if (position.y - colliderHeight < ScreenUtils.ScreenBottom ||
            position.y + colliderHeight > ScreenUtils.ScreenTop)
        {
            position.y *= -1;
        }
        transform.position = position;
    }

    /// <summary>
    /// Initializes collider sizes
    /// </summary>
    void InitColliderSize()
    {
        // initialize colliders
        Collider2D collider = GetComponent<Collider2D>();
        if (collider is BoxCollider2D)
        {
            BoxCollider2D boxCollider2D = (BoxCollider2D)collider;
            colliderWidth = boxCollider2D.size.x;
            colliderHeight = boxCollider2D.size.y;
        }
        else if (collider is CapsuleCollider2D)
        {
            CapsuleCollider2D capsuleCollider2D = (CapsuleCollider2D)collider;
            colliderWidth = capsuleCollider2D.size.x;
            colliderHeight = capsuleCollider2D.size.y;
        }
    }

}
