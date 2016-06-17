using UnityEngine;
using System.Collections;

public class BoltController : MonoBehaviour {

	float boltSpeed = 9.0f;

    public float force;
    public float radius;
    public float jumpFactor;

    public GameObject shatteredSphere;
    private ObjectPoolScript objectPool;

    private GameController gameController;

    void Awake () {       
        gameController = FindObjectOfType<GameController>();
        objectPool = gameController.GetComponent<ObjectPoolScript>();
  
    }

	void OnTriggerEnter (Collider collider) {
		if (collider.tag == "Enemy") {
            Explode(collider.transform.position);
            gameController.GotOne();
            gameObject.SetActive(false);
		}
	}

    void OnDisable() {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    void Explode(Vector3 position) {
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        foreach (Collider collider in colliders) {
            if (collider.GetComponent<Rigidbody>() != null) {
                if (collider.CompareTag("Enemy")) {
                    GameObject brokenSphere = objectPool.GetPooledDestroyedSphere();
                    //TODO ustawic transfomr tutaj a nie w activate
                    DestroyedSphereController brokenSphereController = brokenSphere.GetComponent<DestroyedSphereController>();
                    brokenSphereController.Activate(collider.transform.position, collider.transform.rotation);
                    collider.gameObject.SetActive(false);
                    //GameObject brokenSphere = (GameObject)Instantiate(shatteredSphere, collider.transform.position, collider.transform.rotation);
                    //DestroyObject(collider.gameObject);

                    Rigidbody[] brokenPieces = brokenSphere.GetComponentsInChildren<Rigidbody>();
                    foreach (Rigidbody piece in brokenPieces) {
                        piece.AddExplosionForce(force, position, radius, jumpFactor, ForceMode.Impulse);
                       
                        // Destroy(piece.GetComponent<Collider>(), 9.0f);
                    }

                    brokenSphereController.Deactivate(5.0f);
                    //Destroy(brokenSphere, 10.0f);

                } else {
                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(force, position, radius, jumpFactor, ForceMode.Impulse);
                }
            }
        }
    }
}
