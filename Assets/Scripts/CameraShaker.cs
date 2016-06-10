﻿using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour {
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private float shakeTime = 0.0f;

    Vector3 originalPos;

    void Awake() {
        if (camTransform == null) {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable() {
        originalPos = camTransform.localPosition;
    }

    void Update() {
        if (shakeTime > 0) {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeTime -= Time.deltaTime * decreaseFactor;
        } else {
            shakeTime = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    public void ShakeCamera(float force, float shakeDuration) {
        shakeAmount = force;
        shakeTime = shakeDuration;
    }
}
