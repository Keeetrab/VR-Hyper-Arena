using UnityEngine;
using System.Collections;

public class BoltController : MonoBehaviour {

	float boltSpeed = 9.0f;

    public float force;
    public float radius;
    public float jumpFactor;

    public GameObject shatteredSphere;
    public GameObject hitEffect;
    public GameObject deathEffect;
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
            if(enemy.GetCurrnetHP() <= 1) {

                //Destroy the enemy
                gameController.GotOne(enemy.scoreValue);
                enemy.gameObject.SetActive(false);
            
                

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
            } else {
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


    void Explode(Vector3 position) {
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        foreach (Collider collider in colliders) {
            if (collider.GetComponent<Rigidbody>() != null) {
                if (collider.CompareTag("Enemy")) {
                    GameObject brokenSphere = objectPool.GetPooledExplosion();
                    //TODO ustawic transfomr tutaj a nie w activate
                    DestroyedSphereController brokenSphereController = brokenSphere.GetComponent<DestroyedSphereController>();
                    brokenSphereController.Activate(collider.transform.position, collider.transform.rotation);                
                        
                    if (collider.transform.CompareTag("Enemy")) {
                        collider.gameObject.SetActive(false);                       
                    } else {
                        collider.transform.parent.transform.parent.gameObject.SetActive(false);
                    }

                    Rigidbody[] brokenPieces = brokenSphere.GetComponentsInChildren<Rigidbody>();
                    foreach (Rigidbody piece in brokenPieces) {
                        piece.AddExplosionForce(force, position, radius, jumpFactor, ForceMode.Impulse);             
                    }

                    brokenSphereController.Deactivate(5.0f);


                } else {
                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(force, position, radius, jumpFactor, ForceMode.Impulse);
                }
            }
        }
    }
}
