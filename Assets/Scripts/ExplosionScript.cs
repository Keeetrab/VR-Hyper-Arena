using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

    public float force;
    public float radius;
    public float jumpFactor;

    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast (ray, out hit, 100)) {
                rb.AddExplosionForce(force, hit.point, radius, jumpFactor, ForceMode.Impulse);
            }
        }
	}
}
