using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    #region Variables
    [Header("Starting Lives")]
    [SerializeField] int startingLives;
    [SerializeField] Vector3 startPosition;
    int lives;
    bool canCollide;
    [Header("References")]
    [SerializeField] Text livesText;
    [SerializeField] Image damageFlashImage;
    #endregion

    #region Start
    void Start()
    {
        canCollide = true;
        Respawn();
    }
    #endregion

    #region Update
    void Update()
    {
        // Set the lives text to the current lives
        livesText.text = "Lives: " + lives;
    }
    #endregion

    #region Respawn
    void Respawn()
    {
        // Reset lives
        lives = startingLives;
        // Reset position
        transform.position = startPosition;
    }
    #endregion

    #region DamagePlayer
    void DamagePlayer()
    {
        // Lose a life
        lives -= 1;
        // Damage flash
        damageFlashImage.GetComponent<Animator>().SetTrigger("Damaged");
        Debug.Log(lives);
        if (lives <= 0)
        {
            Dead();
        }
    }
    #endregion

    #region Dead
    void Dead()
    {
        // Game over screen
    }
    #endregion

    #region OnTriggerEnter
    void OnTriggerEnter(Collider other)
    {
        // If collided with an obstacle
        if (other.tag == "Obstacle" && canCollide)
        {
            DamagePlayer();
            // Give invulnerability
            canCollide = false; 
            // Do some flash animation for 1 seconds
        }
    }
    #endregion

    #region OnTriggerExit
    void OnTriggerExit(Collider other)
    {
        // If no longer colliding with the obstacle
        if (other.tag == "Obstacle")
        {
            // Turn off invulnerability
            canCollide = true;
            Debug.Log("No longer invulnerable");
        }
    }
    #endregion
}
