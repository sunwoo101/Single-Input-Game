using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script allows the camera to follow the player on the z axis
 */
public class PlayerFollow : MonoBehaviour
{
    #region Variables
    [Header("Horizontal Offset")]
    [SerializeField] float zOffset;
    [Header("References")]
    [SerializeField] Transform player;
    #endregion

    #region LateUpdate
    void LateUpdate()
    {
        // Follow the player
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + zOffset);
    }
    #endregion
}
