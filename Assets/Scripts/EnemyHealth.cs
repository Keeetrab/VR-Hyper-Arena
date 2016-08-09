using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int startingHP;
    public int scoreValue;
    public GameObject mainGameObject;
    private int currentHP;

    void OnEnable() {
        currentHP = startingHP;
    }

    public int GetCurrnetHP() {
        return currentHP;
    }

    public void DealDamage(int dmg) {
        currentHP -= dmg;     
    }

    public void EnemyDead() {
        if(mainGameObject != null) {
            mainGameObject.SetActive(false);
        } else {
           gameObject.SetActive(false);
        }

    }
}
