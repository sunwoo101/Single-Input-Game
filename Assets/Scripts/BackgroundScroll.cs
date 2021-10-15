using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script handles background scrolling
 */
public class BackgroundScroll : MonoBehaviour
{
    #region Variables
    [SerializeField] Transform endPosition;
    [SerializeField] Transform resetPosition;
    [SerializeField] float moveSpeed;
    // The direction to move
    Vector3 movement;
    #endregion

    #region Update
    void Update()
    {
        // Set the vertical movement
        movement.z = -moveSpeed;
        // Call the movement function
        Move();
    }
    #endregion

    #region Move
    void Move()
    {
        // Move
        transform.position += movement * Time.deltaTime;
        // If off screen
        if (transform.position.z <= endPosition.position.z)
        {
            // Go to reset position
            transform.position = new Vector3(transform.position.x, transform.position.y, resetPosition.position.z);
        }
    }
    #endregion
}
