using UnityEngine;
using UnityEngine.UI;

/*
 * This script handles the player score
 */
public class PlayerScore : MonoBehaviour
{
    #region Variables
    [Header("References")]
    [SerializeField] Text scoreText;
    int score;
    #endregion

    #region Update
    void Update()
    {
        // Set the score text to the current score
        scoreText.text = "Score: " + score + "KM";
    }
    #endregion
}
