using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public bool isGameOver;
	public Text scoreTxt;
    public Canvas VRGameOverCanvas;
    public Text VRGameOverTxt;
    public Canvas playButton;
    public EnemySpawner enemySpawner;
    public CameraShaker cameraShaker;
    public GameObject playerCamera;
    public GameObject playerBody;

    public AudioClip welcomeMusic;
    public float welcomeMusicVolume = 0.4f;
    public AudioClip combatMusic;
    public float combatMusicVolume = 0.6f;

    private AudioSource audioSource;
    private int _currScore;
	private bool _didIWin;


    void Start() {
        VRGameOverCanvas.enabled = false;
        playButton.enabled = true;
        audioSource = this.GetComponent<AudioSource>();

        audioSource.volume = welcomeMusicVolume;
        audioSource.clip = welcomeMusic;
        audioSource.Play();
    }

    /// <summary>
    /// Start a new game.
    /// </summary>
    public void NewGame() {
        playButton.enabled = false;
        audioSource.Stop();
        audioSource.volume = combatMusicVolume;
        audioSource.clip = combatMusic;
        audioSource.Play();

		ResetGame();
        enemySpawner.StartWaves();
	}
	

	/// <summary>
	/// Got an enemy! Increment the score and see if we win.
	/// </summary>
	public void GotOne() {
		_currScore++;
        scoreTxt.text = "Score\n" + _currScore;

	}
	
	/// <summary>
	/// Game's over. 
	/// </summary>
	/// <param name="didIWin">Did the playeer win?</param>	
	public void GameOver(bool didIWin) {
		isGameOver = true;
        _didIWin = didIWin;

        cameraShaker.ShakeCamera(0.7f, 1f);
        Handheld.Vibrate();

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
        VRGameOverCanvas.enabled = false;
        playButton.enabled = false;
		isGameOver = false;
		_currScore = 0;
        scoreTxt.text = "Score\n " + _currScore;

        // Remove any remaining game objects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
        	Destroy(enemy);
        }
        
        GameObject[] ninjaStars = GameObject.FindGameObjectsWithTag("Bolt");
        foreach (GameObject ninjaStar in ninjaStars) {
        	Destroy (ninjaStar);
        }
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
        VRGameOverCanvas.transform.LookAt(new Vector3 (0f,1f,0f));
        VRGameOverTxt.text = finalTxt;
    }

}
