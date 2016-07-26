using UnityEngine;
using System.Collections;

public class SphereDestroyedSoundController : MonoBehaviour {

    public float lowPitchRange = .75F;
    public float highPitchRange = 1.5F;
    public float volume = 0.7f;

    public AudioClip explosionSound;
    public AudioClip rockFallingSounds;
    private AudioSource audioSource;
    

    // Use this for initialization
    void Awake () {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable() {
        audioSource.pitch = Random.Range(lowPitchRange, highPitchRange);
        audioSource.PlayOneShot(rockFallingSounds, volume);
        audioSource.PlayOneShot(explosionSound, volume);
    }
}
