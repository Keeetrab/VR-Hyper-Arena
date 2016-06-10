using UnityEngine;
using System.Collections;

public class SphereDestroyedSoundController : MonoBehaviour {

    public float lowPitchRange = .75F;
    public float highPitchRange = 1.5F;
    public float volume = 0.7f;

    public AudioClip explosionSound;
    public AudioClip rockFallingSounds;
    private AudioSource audio;
    

    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();

        audio.pitch = Random.Range(lowPitchRange, highPitchRange);
        audio.PlayOneShot(rockFallingSounds, volume);
        audio.PlayOneShot(explosionSound, volume + 0.2f);
    }
}
