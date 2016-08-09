using UnityEngine;
using System.Collections;

public class EnemySpiderSoundController : MonoBehaviour {

    public AudioClip walkingSound;
    public AudioClip onFaceSound;
    public AudioClip hitSound;

    private AudioSource[] audioSources;
    private AudioSource audioWalking;
    private AudioSource audioFace;

    void Awake() {
        audioSources = GetComponents<AudioSource>();

        //Walking Sound
        audioWalking = audioSources[0];
        audioWalking.loop = true;
        audioWalking.clip = walkingSound;

        //Sound when drone is on players face
        audioFace = audioSources[1];
        audioFace.loop = true;
        audioFace.pitch = 1.2f;
        audioFace.volume = 0.8f;
        audioFace.clip = onFaceSound;

    }

    public void PlayStabSound() {
        audioFace.PlayOneShot(hitSound, 1.0f);
    }

    public void PlayWalkingSound() {
        if (audioWalking != null) {
            audioWalking.Play();
        }
    }

    public void StopWalkingSound() {
        if (audioWalking != null) {
            audioWalking.Stop();
        }
    }

    public void PlayOnFaceSound() {
        if (audioFace != null) {
            audioFace.volume = 0.8f;
            audioFace.Play();
        }
    }

    public void LowerFaceSoundVolume() {
        if (audioFace != null) {
            audioFace.volume = 0.35f;
        }
    }

    public void StopOnFaceSound() {
        if (audioFace != null) {
            audioFace.Stop();
        }
    }
}
