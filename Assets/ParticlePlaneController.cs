using UnityEngine;
using System.Collections;

public class ParticlePlaneController : MonoBehaviour {

    private ParticleSystem particles;

    void Awake() {
        particles = GetComponent<ParticleSystem>();

        //Set floor for particles to bounce
        particles.collision.SetPlane(0, GameObject.FindGameObjectWithTag("QuadPlane").transform);
    }
}



