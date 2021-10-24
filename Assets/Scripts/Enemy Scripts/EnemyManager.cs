using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This scripts variables for all enemies to access
 */
public class EnemyManager : MonoBehaviour
{
    #region Variables
    public static EnemyManager Instance;
    public float moveSpeedMultiplier;
    #endregion

    #region Awake
    private void Awake()
    {
        Instance = this;
    }
    #endregion
}
