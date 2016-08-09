using UnityEngine;
using System.Collections;

public class SelfDisable : MonoBehaviour {

    public float selfDisableTime;

	void OnEnable() {
        Invoke("Disable", selfDisableTime);
    }

    void Disable() {
        gameObject.SetActive(false);
    }
}
