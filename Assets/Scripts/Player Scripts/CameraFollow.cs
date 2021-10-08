using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
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
