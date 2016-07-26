using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int startingHP;
    public int scoreValue;
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
}
