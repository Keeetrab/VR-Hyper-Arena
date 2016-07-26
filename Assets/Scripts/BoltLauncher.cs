using UnityEngine;
using System.Collections;

public class BoltLauncher : MonoBehaviour {

	public GameObject player;
    public Transform spawnPoint;
	public BoltController bolt;
    public float boltSpeed = 9.0f;
    public float lazerSoundVolume = 0.8f;

	private AudioSource lazerSound;
    private ObjectPoolScript objectPool;
	private GameController gameController;
    private bool canShot;


    void Start () {
        gameController = this.GetComponentInParent<GameController>();
        objectPool = transform.parent.GetComponentInChildren<ObjectPoolScript>();
        lazerSound = GetComponent<AudioSource>();
        lazerSound.volume = lazerSoundVolume;
        canShot = true;
    }
	
	void Update () {
        if (GvrViewer.Instance.VRModeEnabled && GvrViewer.Instance.Triggered && !gameController.isGameOver && canShot) {
            GameObject vrLauncher =
                 GvrViewer.Instance.GetComponentInChildren<GvrHead>().gameObject;
           
            LaunchFrom(vrLauncher);

        } else if (!GvrViewer.Instance.VRModeEnabled && Input.GetButtonDown("Fire1") &&
           !gameController.isGameOver && canShot) {
			Vector3 mouseLoc = Input.mousePosition;
			Vector3 worldMouseLoc = Camera.main.ScreenToWorldPoint(mouseLoc);
			worldMouseLoc.y = player.transform.position.y;
			player.transform.LookAt(worldMouseLoc);
			LaunchFrom(player);
		}	
	}
	
	void LaunchFrom(GameObject origin) {
        //Object Pool

        GameObject bolt = objectPool.GetPooledBolt();

        bolt.transform.position = spawnPoint.position;

        Vector3 direction = player.transform.position - spawnPoint.position;
        bolt.transform.rotation = Quaternion.LookRotation(direction);
        
        bolt.SetActive(true);
        bolt.GetComponent<Rigidbody>().AddForce(direction * - boltSpeed) ;


        // Play a sound effect
        lazerSound.Play();
		
	}

    public void DisableShooting() {
        canShot = false;
    }

    public void EnableShooting() {
        canShot = true;
    }
	
}
