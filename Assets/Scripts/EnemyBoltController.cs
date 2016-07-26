using UnityEngine;
using System.Collections;

public class EnemyBoltController : MonoBehaviour {

    public int damage;

    private AudioSource audioSource;
    private PlayerHealth playerHealth;

    void Awake() {
        playerHealth = FindObjectOfType<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PlayerCube")) {
            playerHealth.TakeDamage(damage);
            audioSource.Play();
            Destroy(gameObject, 2.0f);
            
        }
    }

    void OnDisable() {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
