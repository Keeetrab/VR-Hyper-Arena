using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public AudioClip deathClip;
    public GameController gameController; // The audio clip to play when the player dies.
    public CameraEffectsController cameraEffectsController;

    private AudioSource playerAudio;                                    // Reference to the AudioSource component.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.


    void Awake() {
        // Setting up the references.

        playerAudio = GetComponent<AudioSource>();

        // Set the initial health of the player.
        currentHealth = startingHealth;
    }


    void Update() {

    }

    public void HealPlayer(int amount) {
        currentHealth += amount;

        if(currentHealth > startingHealth) {
            currentHealth = startingHealth;       
        }
        healthSlider.value = currentHealth;
    }


    public void TakeDamage(int amount) {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;

        // Play the hurt sound effect.
        playerAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead) {
            // ... it should die.
            Death();
        }

        cameraEffectsController.HitScreenEffect();
        
    }


    void Death() {
        // Set the death flag so this function won't be called again.
        isDead = true;
        gameController.GameOver(false);

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        playerAudio.clip = deathClip;
        playerAudio.Play();

        cameraEffectsController.DeathScreenEffect();
    }

}