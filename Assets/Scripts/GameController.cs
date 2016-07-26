using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public bool isGameOver;
    public Canvas VRGameOverCanvas;
    public Text VRGameOverTxt;
    public Canvas playButton;
    public EnemySpawner enemySpawner;
    public GameObject playerCamera;
    public GameObject playerBody;
    public PlayerHealth playerHealth;

    public AudioClip welcomeMusic;
    public float welcomeMusicVolume = 0.4f;
    public AudioClip combatMusic;
    public float combatMusicVolume = 0.6f;

    private AudioSource audioSource;
	private bool _didIWin;
    public bool hasSomethingOnFace;
    private ScoreController scoreController;


    void Start() {

        hasSomethingOnFace = false;
        VRGameOverCanvas.enabled = false;
        playButton.enabled = true;
        audioSource = this.GetComponent<AudioSource>();

        audioSource.volume = welcomeMusicVolume;
        audioSource.clip = welcomeMusic;
        audioSource.Play();

        scoreController = GetComponentInChildren<ScoreController>();   
    }

    /// <summary>
    /// Start a new game.
    /// </summary>
    public void NewGame() {
        playButton.enabled = false;
        audioSource.Stop();
        audioSource.volume = combatMusicVolume;
        audioSource.clip = combatMusic;
        audioSource.loop = true;
        audioSource.Play();

		ResetGame();
        
	}
	

	/// <summary>
	/// Got an enemy! Increment the score and see if we win.
	/// </summary>
	public void GotOne(int points) {
        scoreController.AddPoints(points);
        enemySpawner.EnemyKilled();
	}
	
	/// <summary>
	/// Game's over. 
	/// </summary>
	/// <param name="didIWin">Did the playeer win?</param>	
	public void GameOver(bool didIWin) {
		isGameOver = true;
        _didIWin = didIWin;

        enemySpawner.DeactivateAllEnemies();
        Invoke("spawnGameOverUI", 1.5f);

	}
	

	/// <summary>
	/// Resets the interface, removes remaining game objects. Basically gets us to a point
	/// where we're ready to play again.
	/// </summary>
	public void ResetGame() {
        // Reset the interface
        playerBody.transform.position = new Vector3(0, 1, 0);
        playerBody.transform.rotation = Quaternion.identity;
        playerHealth.HealPlayer(playerHealth.startingHealth);
        hasSomethingOnFace = false;
        VRGameOverCanvas.enabled = false;
        playButton.enabled = false;
		isGameOver = false;
        scoreController.ResetScore();

        // Remove any remaining game objects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
        	Destroy(enemy);
        }
        
        GameObject[] ninjaStars = GameObject.FindGameObjectsWithTag("Bolt");
        foreach (GameObject ninjaStar in ninjaStars) {
        	Destroy (ninjaStar);
        }

        //Start Waves
        enemySpawner.StartWaves();
    }

    Vector3 spawnUIInFrontOfCamera() {

        float spawnDistance = 4;

        float rad = playerCamera.transform.eulerAngles.y * Mathf.Deg2Rad;
  


        Vector3 spawnPos = new Vector3(
            spawnDistance * Mathf.Sin(rad),
            VRGameOverCanvas.transform.position.y,
            spawnDistance * Mathf.Cos(rad));
    

        return spawnPos;
    }

    void spawnGameOverUI() {
        string finalTxt = "Game Over";
        VRGameOverCanvas.enabled = true;
        VRGameOverCanvas.transform.position = spawnUIInFrontOfCamera();
        VRGameOverCanvas.transform.LookAt(new Vector3(0f, 1f, 0f));
        VRGameOverTxt.text = finalTxt;
    }

}
