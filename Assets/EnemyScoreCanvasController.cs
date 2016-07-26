using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyScoreCanvasController : MonoBehaviour {

    //Do przeniesienia Score z EnemyHealth

    public Text scoreText;
    public Text comboText;

    private ScoreController scoreController;

    void Awake() {
        scoreController = FindObjectOfType<ScoreController>();
    }

    public void ShowScoreCanvas(int scoreValue) {
        scoreText.text = scoreValue.ToString();
        if (scoreController.GetCombo() > 1) {
            comboText.text = "x " + scoreController.GetCombo().ToString();
        } else {
            comboText.text = "";
        }
        
        gameObject.SetActive(true);
        Invoke("DisableCanvas", 3.0f);
    }

    void DisableCanvas() {
        gameObject.SetActive(false);
    }
}
