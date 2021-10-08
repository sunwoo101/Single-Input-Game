using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add Rigidbody component to the player
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Vertical Speed")]
    [SerializeField] float zSpeed;
    [Header("Horizontal Speed")]
    [SerializeField] float xSpeed;
    // The direction to move
    Vector3 movement;
    [Header("References")]
    Rigidbody rigidBody;
    #endregion

    #region Start
    void Start()
    {
        // Get the rigid body
        rigidBody = GetComponent<Rigidbody>();
    }
    #endregion

    #region Update
    void Update()
    {
        // Set the vertical movement
        movement.z = zSpeed;
        // Set the horizontal movement
        movement.x = Input.GetAxisRaw("Horizontal") * xSpeed;
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
