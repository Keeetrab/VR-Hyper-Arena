using UnityEngine;
using System.Collections;

public class EnemyCollider : MonoBehaviour {

	public GameController gameController;
    public float shakeForceModifier;
    public float shakeTimeModifier;
	
	
	void OnCollisionEnter(Collision collision) {
        Collider other = collision.collider;
        // End the game if an enemy not in the dying state hits us.
        if (other.tag == "Enemy") {
        
			EnemyController badGuy = other.gameObject.GetComponent<EnemyController>();
			if (!badGuy.IsDying()) {
				gameController.GameOver(false);
			}
		}

        if (other.CompareTag("Broken Piece")) {
            float shakeForce = collision.relativeVelocity.magnitude * shakeForceModifier;
            float shakeTime = collision.relativeVelocity.magnitude * shakeTimeModifier;
            gameController.cameraShaker.ShakeCamera(shakeForce, shakeTime);
        }
	}	

}
