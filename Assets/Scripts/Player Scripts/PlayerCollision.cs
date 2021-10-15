using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This script handles player collision
 */
public class PlayerCollision : MonoBehaviour
{
    #region Variables
    [SerializeField] int invulnerabilityTimer;
    [SerializeField] int startingLives;
    [Header("References")]
    [SerializeField] Image damageFlashImage;
    [SerializeField] Text livesText;
    [SerializeField] Transform startPosition;
    bool barrierPowerUp;
    bool boostPowerUp;
    bool slowPowerUp;
    bool vulnerable;
    int lives;
    #endregion

    #region Start
    void Start()
    {
        vulnerable = true;
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
        transform.position = startPosition.position;
    }
    #endregion

    #region DamagePlayer
    void DamagePlayer()
    {
        lives -= 1;
        // Damage flash
        damageFlashImage.GetComponent<Animator>().SetTrigger("Damaged");
        // If the player has no more lives
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
        if (other.tag == "Obstacle" && vulnerable && !barrierPowerUp)
        {
            DamagePlayer();
            vulnerable = false;
            StartCoroutine(VurnerableTrue(invulnerabilityTimer));
            // Make the plyaer flash
        }
        // If collided with an obstacle but player has a barrier
        else if (other.tag == "Obstacle" && barrierPowerUp)
        {
            barrierPowerUp = false;
            // Hide barrier
        }

        // If collided with an item
        if (other.tag == "Item")
        {
            // Destroy the item
            Destroy(other.gameObject);
        }
    }
    #endregion

    #region VurnerableTrue
    // This function is to re enable vulnerability after x seconds
    IEnumerator VurnerableTrue(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        vulnerable = true;
    }
    #endregion
}
