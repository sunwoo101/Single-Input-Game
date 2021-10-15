using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is for the obstacles
 */
public class ObstacleMovement : MonoBehaviour
{
    #region Variables
    public static ObstacleMovement Instance;
    [SerializeField] float destroyTimer;
    public float moveSpeed;
    // The direction to move
    Vector3 movement;
    #endregion

    #region Awake
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Start
    void Start()
    {
        Destroy(gameObject, destroyTimer);
    }
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
    }
    #endregion
}
