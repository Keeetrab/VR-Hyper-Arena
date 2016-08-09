using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Canvas VRGameOverCanvas;
    public Text VRGameOverTxt;
    public GameObject tutorialCanvas;
    public Button tutorialButton;
    public bool isGameOver;
    public Canvas playButton;
    public EnemySpawner enemySpawner;
    public GameObject playerCamera;
    public GameObject playerBody;
    public PlayerHealth playerHealth;

    public AudioClip welcomeMusic;
    public float welcomeMusicVolume = 0.4f;
    public AudioClip combatMusic;
    public float combatMusicVolume = 0.6f;
    public float combatMusciPitch = 1.2f;

    private AudioSource audioSource;
    public bool hasSomethingOnFace;
    private ScoreController scoreController;
    


    void Start() {

        isGameOver = false;
        hasSomethingOnFace = false;
        VRGameOverCanvas.enabled = false;
        playButton.enabled = true;
        tutorialButton.enabled = true;
        audioSource = this.GetComponent<AudioSource>();

        audioSource.volume = welcomeMusicVolume;
        audioSource.clip = welcomeMusic;
        audioSource.Play();

        scoreController = GetComponentInChildren<ScoreController>();   
    }

    // New Game

    public void NewGame() {
        playButton.enabled = false;
        audioSource.Stop();
        audioSource.volume = combatMusicVolume;
        audioSource.clip = combatMusic;
        audioSource.pitch = combatMusciPitch;
        audioSource.loop = true;
        audioSource.Play();

		ResetGame();
        
	}
	
    // Enemy killed, add points and increase amount of killed enemies in wave.
	public void GotOne(int points) {
        scoreController.AddPoints(points);
        enemySpawner.EnemyKilled();
	}
	

    // Game Over, spawn Game Over UI after camera effects

	public void GameOver() {
        isGameOver = true;
        Invoke("SpawnGameOverUI", 1.5f);
        enemySpawner.DeactivateAllEnemies();     

	}
	

	// Reset everything and start game again

	public void ResetGame() {
        // Reset the interface
        playerBody.transform.position = new Vector3(0, 1, 0);
        playerBody.transform.rotation = Quaternion.identity;
        playerHealth.HealPlayer(playerHealth.startingHealth);
        hasSomethingOnFace = false;
        HideCanvases();
        scoreController.ResetScore();

        // Remove any remaining game objects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
        	Destroy(enemy);
        }
        
        GameObject[] remainedBolts = GameObject.FindGameObjectsWithTag("Bolt");
        foreach (GameObject ninjaStar in remainedBolts) {
        	Destroy (ninjaStar);
        }

        // Start Waves
        isGameOver = false;
        enemySpawner.StartWaves();
    }



    Vector3 SpawnUIInFrontOfCamera() {

        float spawnDistance = 4;

        float rad = playerCamera.transform.eulerAngles.y * Mathf.Deg2Rad;
  


        Vector3 spawnPos = new Vector3(
            spawnDistance * Mathf.Sin(rad),
            VRGameOverCanvas.transform.position.y,
            spawnDistance * Mathf.Cos(rad));
    

        return spawnPos;
    }

    void HideCanvases() {
        VRGameOverCanvas.enabled = false;
        tutorialCanvas.SetActive(false);
        playButton.enabled = false;
        tutorialButton.gameObject.SetActive(false);
    }

    void SpawnGameOverUI() {
        string finalTxt = "Game Over";
        VRGameOverCanvas.enabled = true;
        VRGameOverCanvas.transform.position = SpawnUIInFrontOfCamera();
        VRGameOverCanvas.transform.LookAt(new Vector3(0f, 1f, 0f));
        VRGameOverTxt.text = finalTxt;
    }

    public void ShowTutorial() {
        tutorialButton.gameObject.SetActive(false);
        tutorialCanvas.SetActive(true);
    }

}
