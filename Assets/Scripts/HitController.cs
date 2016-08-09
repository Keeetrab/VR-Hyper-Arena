using UnityEngine;
using System.Collections;

public class HitController : MonoBehaviour {
    public float selfDisableTime;
    private AudioSource audioSource;
    public float pitchMinRange;
    public float pitchMaxRange;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable() {
        audioSource.pitch = Random.Range(pitchMinRange, pitchMaxRange);
        Invoke("Disable", selfDisableTime);
    }

    void Disable() {
        gameObject.SetActive(false);
    }
}
