using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This script is for the Main Menu
 */
public class MainMenu : MonoBehaviour
{
    #region Play
    public void Play()
    {
        // Set time scale to 1
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
    #endregion
    
    #region ExitGame
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    #endregion
}
