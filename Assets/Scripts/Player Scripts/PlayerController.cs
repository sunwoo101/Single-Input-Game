using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Add Rigidbody component to the player
[RequireComponent(typeof(Rigidbody))]

/*
 * This script handles the player
 */
public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] float moveSpeed;
    [SerializeField] int invulnerabilityTimer;
    [SerializeField] int startingLives;
    [SerializeField] float slowPowerUpDuration;
    [SerializeField] float barrierPowerUpDuration;
    [SerializeField] float boostPowerUpDuration;
    [SerializeField] float slowPowerUpMultiplier;
    [SerializeField] float boostPowerUpMultiplier;
    [Header("References")]
    [SerializeField] Transform clampMinPos;
    [SerializeField] Transform clampMaxPos;
    [SerializeField] GameObject barrierPowerUpModel;
    [SerializeField] GameObject boostPowerUpModel;
    [SerializeField] Image damageFlashImage;
    [SerializeField] Text livesText;
    [SerializeField] Transform startPosition;
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] Text scoreText;
    [SerializeField] Text finalScoreText;
    [SerializeField] Text highScoreText;
    float score;
    float scoreMultiplier;
    float highScore;
    float ticks;
    bool slowPowerUp;
    bool barrierPowerUp;
    bool boostPowerUp;
    bool vulnerable;
    int lives;

    // The direction to move
    Vector3 movement;
    #endregion

    #region Start
    void Start()
    {
        scoreMultiplier = 1;
        highScore = PlayerPrefs.GetFloat("highScore", 0);
        vulnerable = true;
        Respawn();
    }
    #endregion

    #region Update
    void Update()
    {
        ticks += Time.deltaTime;
        if (ticks >= 1)
        {
            score += 1 * scoreMultiplier;
            ticks = 0;
        }
        // Set the horizontal movement
        movement.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        // Call the movement function
        Move();
        // Set the score text to the current score
        scoreText.text = "Score: " + score;
        // Show/hide barrier
        if (barrierPowerUp)
        {
            barrierPowerUpModel.SetActive(true);
        }
        else
        {
            barrierPowerUpModel.SetActive(false);
        }
        // Show/hide boost shield
        if (boostPowerUp)
        {
            boostPowerUpModel.SetActive(true);
        }
        else
        {
            boostPowerUpModel.SetActive(false);
        }
    }
    #endregion

    #region Move
    void Move()
    {
        // Move the player
        transform.position += movement * Time.deltaTime;
        // Clamp the position
        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, clampMinPos.position.x, clampMaxPos.position.x);
        transform.position = clampedPos;
        // Steer animation
        GetComponent<Animator>().SetFloat("Direction", movement.x);
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
        // Set the lives text to the current lives
        livesText.text = "Lives: " + lives;
    }
    #endregion

    #region Dead
    void Dead()
    {
        // Set new high score
        if (score > highScore)
        {
            PlayerPrefs.SetFloat("highScore", score);
        }
        // Set time scale to 0
        Time.timeScale = 0;
        // Show game over menu
        highScoreText.text = "HighScore: " + highScore;
        finalScoreText.text = "Score: " + score;
        gameOverMenu.SetActive(true);
    }
    #endregion

    #region OnTriggerEnter
    void OnTriggerEnter(Collider other)
    {
        
        // If collided with an obstacle
        if (other.CompareTag("Obstacle") && vulnerable && !barrierPowerUp && !boostPowerUp)
        {
            DamagePlayer();
            vulnerable = false;
            StartCoroutine(VurnerableTrue(invulnerabilityTimer));
            // Make the plyaer flash
        }
        // If collided with an obstacle but player has boost power up
        else if (other.CompareTag("Obstacle") && boostPowerUp)
        {
            Destroy(other.gameObject);
        }
        // If collided with an obstacle but player has a barrier
        else if (other.CompareTag("Obstacle") && barrierPowerUp)
        {
            barrierPowerUp = false;
            Destroy(other.gameObject);
            // Hide barrier
            barrierPowerUpModel.SetActive(false);
        }

        // If collided with an item
        if (other.CompareTag("Item"))
        {
            if (other.gameObject.name == "SlowPowerUp(Clone)")
            {
                Debug.Log("slow");
                StartCoroutine(SlowPowerUp(slowPowerUpDuration, slowPowerUpMultiplier));
            }
            if (other.gameObject.name == "BarrierPowerUp(Clone)")
            {
                Debug.Log("barrier");
                StartCoroutine(BarrierPowerUp(barrierPowerUpDuration));
            }
            if (other.gameObject.name == "BoostPowerUp(Clone)")
            {
                Debug.Log("boost");
                StartCoroutine(BoostPowerUp(boostPowerUpDuration, boostPowerUpMultiplier));
            }
            // Destroy the item
            Destroy(other.gameObject);
        }
    }
    #endregion

    #region SlowPowerUp
    // Slow down enemies power up
    IEnumerator SlowPowerUp(float duration, float multiplier)
    {
        slowPowerUp = true;
        EnemyManager.Instance.moveSpeedMultiplier *= multiplier;
        yield return new WaitForSeconds(duration);
        EnemyManager.Instance.moveSpeedMultiplier /= multiplier;
        slowPowerUp = false;
    }
    #endregion

    #region BarrierPowerUp
    IEnumerator BarrierPowerUp(float duration)
    {
        barrierPowerUp = true;
        yield return new WaitForSeconds(duration);
        barrierPowerUp = false;
    }
    #endregion
    
    #region BoostPowerUp
    IEnumerator BoostPowerUp(float duration, float multiplier)
    {
        boostPowerUp = true;
        EnemyManager.Instance.moveSpeedMultiplier *= multiplier;
        scoreMultiplier *= multiplier;
        yield return new WaitForSeconds(duration);
        EnemyManager.Instance.moveSpeedMultiplier /= multiplier;
        scoreMultiplier /= multiplier;
        boostPowerUp = false;
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
