using UnityEngine;
using System.Collections;

public class FlyingEnemyController : MonoBehaviour {

    public float boltSpeed;
    public Transform boltSpawnPoint;
    public Transform player;
    public GameObject bolt;
    private Rigidbody rb;
    private ObjectPoolScript objectPool;

    private Animator anim;
    private int attackTriggerHash = Animator.StringToHash("AttackTrigger");

    private AudioSource engineAudioSource;
    private AudioSource lazerChargingAudioSource;
    private AudioSource lazerShotAudioSource;

    public AudioClip lazerShotSound;
    public float lazerShotVolume = 0.8f;
    public AudioClip lazerChargingSound;
    public float lazerChargingVolume = 0.8f;
    public AudioClip engineSound;
    public float engineVolume = 0.8f;

    public GameObject shotEffect;

    


    void Awake() {
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        objectPool = FindObjectOfType<ObjectPoolScript>();
        player = GameObject.FindGameObjectWithTag("Target").transform;


        //Setting AudioClips for Beep and Lazer Charging. Shooting LazerSound is in bolt
        AudioSource[] audioSources = GetComponents<AudioSource>();

        engineAudioSource = audioSources[0];
        AssignClipToAudioSource(engineAudioSource, engineSound, engineVolume);
        engineAudioSource.loop = true;
        

        lazerChargingAudioSource = audioSources[1];
        AssignClipToAudioSource(lazerChargingAudioSource, lazerChargingSound, lazerChargingVolume);

        lazerShotAudioSource = audioSources[2];
        AssignClipToAudioSource(lazerShotAudioSource, lazerShotSound, lazerShotVolume);

    }

    void OnEnable() {
        engineAudioSource.Play();
    }


    void OnDisable() {
        if (rb == null) {
            rb = this.GetComponent<Rigidbody>();
        }
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        engineAudioSource.Stop();
        DisableShotEffect();
    }


    void ShotAtPlayer() {
        if (this.isActiveAndEnabled) {

            lazerShotAudioSource.Play();

            Vector3 direction = player.position - boltSpawnPoint.position;

            GameObject bullet = objectPool.GetPooledEnemyBolt();

            bullet.transform.position = boltSpawnPoint.position;
            bullet.transform.rotation = Quaternion.LookRotation(direction);
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().AddRelativeForce(bolt.transform.forward * boltSpeed);

        }
    }

    void PlayChargeLazerSound() {

        ActivateShotEffect();
        lazerChargingAudioSource.Play();
    }

    public void AttackPlayer() {
        if (this.isActiveAndEnabled) {
            anim.SetTrigger(attackTriggerHash);
        }
    }

    void AssignClipToAudioSource(AudioSource source, AudioClip clip, float volume) {
        source.clip = clip;
        source.volume = volume;
    }

    void ActivateShotEffect() {
        shotEffect.SetActive(true);
        Invoke("DisableShotEffect", 6.0f);
    }

    void DisableShotEffect() {
        shotEffect.SetActive(false);
    }
}
