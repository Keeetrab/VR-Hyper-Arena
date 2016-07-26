using UnityEngine;
using System.Collections;

public class FlyingEnemyMoverController : MonoBehaviour {

    private FlyingEnemyController enemy;

    void Start() {
        enemy = GetComponentInChildren<FlyingEnemyController>();
    }

    void Attack() {
        enemy.AttackPlayer();
    }
}
