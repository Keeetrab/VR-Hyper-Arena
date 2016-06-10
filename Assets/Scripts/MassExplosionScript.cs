using UnityEngine;
using System.Collections;

public class MassExplosionScript : MonoBehaviour {

    public float force;
    public float radius;
    public float jumpFactor;

    public GameObject shatteredSphere;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Explode(Vector3 position) {
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        foreach (Collider collider in colliders) {
            if (collider.GetComponent<Rigidbody>() != null) {
                if (collider.CompareTag("Enemy")) {
                    GameObject brokenSphere = (GameObject)Instantiate(shatteredSphere, collider.transform.position, collider.transform.rotation);
                    DestroyObject(collider.gameObject);

                    Rigidbody[] brokenPieces = brokenSphere.GetComponentsInChildren<Rigidbody>();
                    foreach (Rigidbody piece in brokenPieces) {
                        piece.AddExplosionForce(force, position, radius, jumpFactor, ForceMode.Impulse);
                    }

                } else {
                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(force, position, radius, jumpFactor, ForceMode.Impulse);
                }
            }
        }
    }
}
