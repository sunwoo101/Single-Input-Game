using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    #region Variables
    [Header("References")]
    [SerializeField] Text scoreText;
    #endregion

    #region Update
    void Update()
    {
        // Distance from start position and current position
        float distance = Mathf.Round(transform.position.z);
        // Set the score text to the current distance
        scoreText.text = "Score: " + distance + "KM";
    }
    #endregion
}
