using UnityEngine;
using System.Collections;

public class FadeText : MonoBehaviour {

    CanvasGroup canvasGroup;
    void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Fade() {
        StartCoroutine(DoFade());
    }

    IEnumerator DoFade() {
        
        while(canvasGroup.alpha>0) {
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }
        canvasGroup.interactable = false;
        yield return null;
    }

    public void Show() {
        canvasGroup.alpha = 100;
    }

}
