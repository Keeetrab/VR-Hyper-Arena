using UnityEngine;
using System.Collections;

public class DestroyedSphereController : MonoBehaviour {

    private Vector3[] originalPositions;
    private Transform[] children;

    private int oldLayer;
    private int voidLayer;

	// Use this for initialization
	void Awake () {
        originalPositions = new Vector3[transform.childCount];
        children = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++) {
            children[i] = transform.GetChild(i);
            originalPositions[i] = transform.position - children[i].transform.position;
            
        }

        voidLayer = LayerMask.NameToLayer("Void");
        oldLayer = LayerMask.NameToLayer("Default");
    }
	
   public void Activate(Vector3 position, Quaternion rotation) {
        Reset(position, rotation);
        gameObject.SetActive(true);

    }

    void Reset(Vector3 Vposition, Quaternion QRotation) {
        for (int i = 0; i < children.Length; i++) {
            Transform child = children[i];

            child.position = Vposition + originalPositions[i];
            child.rotation = Quaternion.identity;
        }

    }

    public void Deactivate(float seconds) {
        //Destroy collider used to make pieces smoothly disappear from screen
        Invoke("DisableCollider", seconds - 2.0f);

        //TODO: Inaczej wybucha jak uzywamy ToggleCollider
        Invoke("Destroy", seconds);
    }
    
    void Destroy() {
        
        gameObject.SetActive(false);
        EnableCollider();
    }

    void DisableCollider() {
        foreach(Transform child in children) {
            child.gameObject.layer = voidLayer;
        }
    }

    void EnableCollider() {
        foreach (Transform child in children) {
            child.gameObject.layer = oldLayer;
            //Reset the velocity
            child.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            child.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
      
}
