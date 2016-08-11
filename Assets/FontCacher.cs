using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FontCacher : MonoBehaviour {

    public Text TextField;
    public string glyphs = "1234567890x";

    protected void Awake() {
        if (TextField == null) {
            TextField = gameObject.GetComponent<Text>();
        }
        StartCoroutine(CacheFont());
    }

    protected IEnumerator CacheFont() {
        Font font = TextField.font;
        font.RequestCharactersInTexture(glyphs, TextField.fontSize, TextField.fontStyle);

        yield break;
    }
}
