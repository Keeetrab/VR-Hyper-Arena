using UnityEngine;
using System.Collections;
using Destruction;

public class EnemyController : MonoBehaviour {

	public float speedMin;
	public float speedMax;
    public float beepInterval;
	
	
	enum EnemyState { Normal, Dying};
	
	private EnemyState _state;
    private Rigidbody rb;
 

    public AudioSource beepAudioSource;
    public float beepVolume = 0.8f;

    private float nextBeep;

    void Awake() {
        if (rb = null) {
            rb = this.GetComponent<Rigidbody>();
        }
        beepAudioSource.volume = beepVolume;
    }

    // Use this for initialization.
    void OnEnable () {
		_state = EnemyState.Normal;
        nextBeep = Time.time + beepInterval;

        float thisSpeed = Random.Range (speedMin, speedMax);
        
        thisSpeed *= 0.85f;

        Vector3 dir = (-this.transform.position).normalized * thisSpeed;
        if (rb != null) {
            rb.velocity = dir;
        }

	}

    void OnDisable() {
        if(rb == null) {
            rb = this.GetComponent<Rigidbody>();
        }
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Update() {
        if(Time.time > nextBeep && _state == EnemyState.Normal) {
            nextBeep = Time.time + beepInterval;
            beepAudioSource.Play();
        }
    }
	
	
	/// <summary>
	/// Check and see if our enemy is in a dying state. We need this because occasionally
	/// momentum drives a "dead" enemy through the end zone.
	/// </summary>
	/// <returns><c>true</c> if this enemy is dying; otherwise, <c>false</c>.</returns>
	public bool IsDying() {
		return (_state == EnemyState.Dying);
	}
	
}

