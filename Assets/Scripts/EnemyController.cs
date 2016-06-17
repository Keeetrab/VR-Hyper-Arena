﻿using UnityEngine;
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

    private float _nextBeep;

    void OnAwake() {
        if (rb = null) {
            rb = this.GetComponent<Rigidbody>();
        }
    }

    // Use this for initialization.
    void OnEnable () {
		_state = EnemyState.Normal;
        _nextBeep = Time.time + beepInterval;

        float thisSpeed = Random.Range (speedMin, speedMax);
        if (GvrViewer.Instance.VRModeEnabled) {
            thisSpeed *= 0.85f;
        }

        
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
        if(Time.time > _nextBeep && _state == EnemyState.Normal) {
            _nextBeep = Time.time + beepInterval;
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

