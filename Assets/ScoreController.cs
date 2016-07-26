using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour {

    public float comboTime;
    private float lastKill;

    private int score;
    private int comboMultiplier = 0;
    private bool killingSpree;

    public Text scoreTxt;
    

    void Start () {
        UpdateScoreText();
	}
	
	void Update () {
	    if(lastKill + comboTime < Time.time) {
            comboMultiplier = 0;
        }
	}

    public void ResetScore() {
        score = 0;
        comboMultiplier = 0;
        UpdateScoreText();
    }

    public int GetCurrentScore() {
        return score;
    }

    public void AddPoints(int points) {
        AddCombo();
        score += points * comboMultiplier;
        UpdateScoreText();       
    }

    public int GetCombo() {
        return comboMultiplier;
    }

    void UpdateScoreText() {
        scoreTxt.text = "Score\n" + score;
    }

    void AddCombo() {
        comboMultiplier += 1;
        lastKill = Time.time; 
    }
}
