using UnityEngine;
using System.Collections;

public class DestroyedSphereController : MonoBehaviour {

    private Vector3[] originalPositions;

	// Use this for initialization
	void Start () {
        originalPositions = new Vector3[transform.childCount];

        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            originalPositions[i] = transform.position - child.transform.position;
        }

    }
	
   public void Activate(Vector3 position, Quaternion rotation) {
        Reset(position, rotation);
        gameObject.SetActive(true);

    }

    void Reset(Vector3 Vposition, Quaternion QRotation) {
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            child.transform.position = Vposition + originalPositions[i];
            child.transform.rotation = QRotation;
        }

    }
}
