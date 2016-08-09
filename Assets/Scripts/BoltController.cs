using UnityEngine;
using System.Collections;

public class BoltController : MonoBehaviour {

    private ObjectPoolScript objectPool;

    private GameController gameController;

    void Awake () {       
        gameController = FindObjectOfType<GameController>();
        objectPool = gameController.GetComponentInChildren<ObjectPoolScript>();
  
    }

	void OnTriggerEnter (Collider collider) {
		if (collider.CompareTag("Enemy")) {
            enemyHit(collider);
            // Explode(collider.transform.position);
           
            gameObject.SetActive(false);
		}
	}

    void enemyHit(Collider collider) {
            EnemyHealth enemy =  collider.GetComponent<EnemyHealth>();
            // If enemy has 1 health (would have 0 with this shot)
            if(enemy.GetCurrnetHP() <= 1) {

                //Destroy the enemy
                gameController.GotOne(enemy.scoreValue);
                enemy.EnemyDead();
            

                //Show floating score text
                GameObject scoreCanvas = objectPool.GetScoreCanvas();
                scoreCanvas.transform.position = collider.transform.position;
                scoreCanvas.transform.LookAt(new Vector3 (0, 1, 0));
                scoreCanvas.transform.Rotate(new Vector3(0, 180, 0));
                int scoreValue = enemy.scoreValue;
                scoreCanvas.GetComponent<EnemyScoreCanvasController>().ShowScoreCanvas(scoreValue);
                scoreCanvas.SetActive(true);

                //Show explosion
                GameObject explosion = objectPool.GetPooledExplosion();
                explosion.transform.position = collider.transform.position;
                explosion.SetActive(true);   
                         
            } else {    // Else deal damage and show hit effect

                enemy.DealDamage(1);

                GameObject hitEffect = objectPool.GetPooledHitEffect();
                hitEffect.transform.position = collider.transform.position;
                hitEffect.SetActive(true);              
        }
    }

    void OnDisable() {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
