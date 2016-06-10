using UnityEngine;
using System.Collections;

public class BoltLauncher : MonoBehaviour {

	public GameObject player;
	public BoltController bolt;
	public AudioSource whooshSound;

	private GameController _gameController;
	private Vector3 _shooterOffset;
    private Vector3 _vrShooterOffset;

    void Start () {
        _gameController = this.GetComponentInParent<GameController>();
		_shooterOffset = new Vector3(0.0f, 0.8f, 1.0f);
        _vrShooterOffset = new Vector3(0.0f, -0.4f, 1.0f);

    }
	
	void Update () {
        if (GvrViewer.Instance.VRModeEnabled && GvrViewer.Instance.Triggered && !_gameController.isGameOver) {
            GameObject vrLauncher =
                 GvrViewer.Instance.GetComponentInChildren<GvrHead>().gameObject;
           
            LaunchNinjaStarFrom(vrLauncher, _vrShooterOffset);

        } else if (!GvrViewer.Instance.VRModeEnabled && Input.GetButtonDown("Fire1") &&
           !_gameController.isGameOver) {
			// First, turn the ninja so he's looking at the player's mouse / finger.
			Vector3 mouseLoc = Input.mousePosition;
			Vector3 worldMouseLoc = Camera.main.ScreenToWorldPoint(mouseLoc);
			worldMouseLoc.y = player.transform.position.y;
			player.transform.LookAt(worldMouseLoc);
			LaunchNinjaStarFrom(player, _shooterOffset);
		}	
	}
	
	void LaunchNinjaStarFrom(GameObject origin, Vector3 shooterOffset) {
		
		// This will a ninja star slightly in front of our origin object.
		// We also have to rotate our model 90 degrees in the x-coordinate.
		Vector3 ninjaStarRotation = origin.transform.rotation.eulerAngles;
		ninjaStarRotation.x = 90.0f;
		Vector3 transformedOffset = origin.transform.rotation * shooterOffset;
		Instantiate(bolt, origin.transform.position + transformedOffset, Quaternion.Euler(ninjaStarRotation));
		
		// Play a sound effect!
		whooshSound.Play();
		
	}
	
}
