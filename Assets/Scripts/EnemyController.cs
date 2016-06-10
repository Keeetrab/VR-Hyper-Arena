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

    private float _nextBeep;

    // Use this for initialization.
    void Start () {
		_state = EnemyState.Normal;
        _nextBeep = Time.time + beepInterval;

        rb = this.GetComponent<Rigidbody>();

        float thisSpeed = Random.Range (speedMin, speedMax);
        if (GvrViewer.Instance.VRModeEnabled) {
            thisSpeed *= 0.85f;
        }

        
        Vector3 dir = (-this.transform.position).normalized * thisSpeed;
        rb.velocity = dir;

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

