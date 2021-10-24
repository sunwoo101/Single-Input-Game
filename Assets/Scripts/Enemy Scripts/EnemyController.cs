using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is for the enemies
 */
public class EnemyController : MonoBehaviour
{
    #region Variables

    [SerializeField] float destroyTimer;
    [SerializeField] float shootTimer;
    [SerializeField] bool canShoot;

    [Header("References")]
    [SerializeField] GameObject bullet;
    [SerializeField] float moveSpeed;

    private float ms;

    // The direction to move
    Vector3 movement;

    #endregion

    #region Start

    void Start()
    {
        Destroy(gameObject, destroyTimer);
        // Call the Shoot function every shootTimer seconds
        if (canShoot)
        {
            InvokeRepeating("Shoot", shootTimer, shootTimer);
        }
    }

    #endregion

    #region Update

    void Update()
    {
        ms = moveSpeed * EnemyManager.Instance.moveSpeedMultiplier;
        // Set the vertical movement
        movement.z = -ms;
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

    #region Shoot
    void Shoot()
    {
        // Set the spawn position
        Vector3 spawnPos = transform.position;
        // Spawn the obstacle
        Instantiate(bullet, spawnPos, Quaternion.identity);
    }
    #endregion
}