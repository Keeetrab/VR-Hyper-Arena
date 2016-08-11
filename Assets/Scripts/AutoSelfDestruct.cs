using UnityEngine;
using System.Collections;

public class AutoSelfDestruct : MonoBehaviour {

    public float time;
    public bool destroyAfterFirstFrame;

	// Use this for initialization
	void Start () {
        if (destroyAfterFirstFrame) {
            StartCoroutine(DestroyAfterFirstFrame());
        } else {
            Destroy(gameObject, time);
        }
	}


    IEnumerator DestroyAfterFirstFrame() {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }


}
