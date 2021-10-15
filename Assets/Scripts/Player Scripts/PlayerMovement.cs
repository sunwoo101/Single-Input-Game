using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add Rigidbody component to the player
[RequireComponent(typeof(Rigidbody))]

/*
 * This script handles player movement
 */
public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Speed")]
    [SerializeField] float moveSpeed;
    // The direction to move
    Vector3 movement;
    [Header("References")]
    PlayerCollision playerHandler;
    Rigidbody rigidBody;
    #endregion

    #region Start
    void Start()
    {
        // Get the rigid body
        rigidBody = GetComponent<Rigidbody>();
        playerHandler = GetComponent<PlayerCollision>();
    }
    #endregion

    #region Update
    void Update()
    {
        // Set the horizontal movement
        movement.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        // Call the movement function
        Move();
    }
    #endregion

    #region Move
    void Move()
    {
        // Move the player
        transform.position += movement * Time.deltaTime;
    }
    #endregion
}
