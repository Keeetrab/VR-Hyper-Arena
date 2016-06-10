using UnityEngine;
using System.Collections;

public class BoltController : MonoBehaviour {

	float starSpeed = 9.0f;
	float rotationSpeed = 8.0f;

    public float force;
    public float radius;
    public float jumpFactor;

    public GameObject shatteredSphere;

    private GameController gameController;

    void Start () {
		this.GetComponent<Rigidbody>().velocity = this.transform.up * starSpeed;
        //this.GetComponent<Rigidbody>().angularVelocity = this.transform.forward * rotationSpeed;
        gameController = FindObjectOfType<GameController>();
  
    }
	
	void OnTriggerEnter (Collider collider) {
		if (collider.tag == "Enemy") {
            Explode(collider.transform.position);
            gameController.GotOne();
			Destroy(gameObject);
		}
	}

    void Explode(Vector3 position) {
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        foreach (Collider collider in colliders) {
            if (collider.GetComponent<Rigidbody>() != null) {
                if (collider.CompareTag("Enemy")) {
                    GameObject brokenSphere = (GameObject)Instantiate(shatteredSphere, collider.transform.position, collider.transform.rotation);
                    DestroyObject(collider.gameObject);

                    Rigidbody[] brokenPieces = brokenSphere.GetComponentsInChildren<Rigidbody>();
                    foreach (Rigidbody piece in brokenPieces) {
                        piece.AddExplosionForce(force, position, radius, jumpFactor, ForceMode.Impulse);
                        Destroy(piece.GetComponent<Collider>(), 9.0f);
                    }
                    Destroy(brokenSphere, 10.0f);

                } else {
                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(force, position, radius, jumpFactor, ForceMode.Impulse);
                }
            }
        }
    }
	
}
