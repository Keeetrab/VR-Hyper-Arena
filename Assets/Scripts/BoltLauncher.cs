using UnityEngine;
using System.Collections;

public class BoltLauncher : MonoBehaviour {

	public GameObject player;
	public BoltController bolt;
    public float boltSpeed = 9.0f;

	private AudioSource lazerSound;
    private ObjectPoolScript objectPool;
	private GameController gameController;
	private Vector3 shooterOffset;
    private Vector3 vrShooterOffset;

    void Start () {
        gameController = this.GetComponentInParent<GameController>();
        objectPool = gameController.GetComponent<ObjectPoolScript>();
        lazerSound = GetComponent<AudioSource>();
		shooterOffset = new Vector3(0.0f, 0.8f, 1.0f);
        vrShooterOffset = new Vector3(0.0f, -0.4f, 1.0f);

    }
	
	void Update () {
        if (GvrViewer.Instance.VRModeEnabled && GvrViewer.Instance.Triggered && !gameController.isGameOver) {
            GameObject vrLauncher =
                 GvrViewer.Instance.GetComponentInChildren<GvrHead>().gameObject;
           
            LaunchFrom(vrLauncher, vrShooterOffset);

        } else if (!GvrViewer.Instance.VRModeEnabled && Input.GetButtonDown("Fire1") &&
           !gameController.isGameOver) {
			// First, turn the ninja so he's looking at the player's mouse / finger.
			Vector3 mouseLoc = Input.mousePosition;
			Vector3 worldMouseLoc = Camera.main.ScreenToWorldPoint(mouseLoc);
			worldMouseLoc.y = player.transform.position.y;
			player.transform.LookAt(worldMouseLoc);
			LaunchFrom(player, shooterOffset);
		}	
	}
	
	void LaunchFrom(GameObject origin, Vector3 shooterOffset) {
		
		Vector3 boltRotation = origin.transform.rotation.eulerAngles;
		boltRotation.x = 90.0f;
        boltRotation.z = 0f;
		Vector3 transformedOffset = origin.transform.rotation * shooterOffset;

        //Object Pool

        GameObject bolt = objectPool.GetPooledBolt();

        
        bolt.transform.position = new Vector3 (origin.transform.position.x + transformedOffset.x, 1.2f, origin.transform.position.z + transformedOffset.z);
       
        bolt.transform.rotation = Quaternion.Euler(boltRotation);
        bolt.SetActive(true);
        bolt.GetComponent<Rigidbody>().velocity = bolt.transform.up * boltSpeed;


        // Play a sound effect
        lazerSound.Play();
		
	}
	
}
