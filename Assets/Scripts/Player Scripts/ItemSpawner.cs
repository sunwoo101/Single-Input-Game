using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script spanws in items
 */
public class ItemSpawner : MonoBehaviour
{
    #region Variables
    [SerializeField] float spawnTimer;
    [Header("References")]
    [SerializeField] GameObject[] items;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    #endregion

    #region Start
    void Start()
    {
        // Call the Spawn function every spawnTimer seconds
        InvokeRepeating("Spawn", spawnTimer, spawnTimer);
    }
    #endregion

    #region Spawn
    void Spawn()
    {
        // Choose a random obstacle fom the obstacles list
        GameObject obstacle = items[Random.Range(0, items.Length)];
        // Choose a random spawn position between pointA and pointB
        Vector3 spawnPos = Vector3.Lerp(pointA.position, pointB.position, Random.Range(0f, 1f));
        // Spawn the obstacle
        Instantiate(obstacle, spawnPos, Quaternion.identity);
    }
    #endregion
}
