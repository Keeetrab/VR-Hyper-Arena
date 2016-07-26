using UnityEngine;
using System.Collections;

public class CameraEffectsController : MonoBehaviour {

    public float hitEffectDuration;
    public float deathEffectDuration;

    bool deathEffectOn = false;

    public Kino.AnalogGlitch analogGlitch;

    public void HitScreenEffect() {
        analogGlitch.enabled = true;
        if (!deathEffectOn) {
            analogGlitch.colorDrift = 0.3f;
            analogGlitch.scanLineJitter = 0.3f;

            Invoke("TurnOffScreenEffects", hitEffectDuration);
        }  
    }

    void TurnOffScreenEffects() {
        analogGlitch.enabled = false;
        analogGlitch.colorDrift = 0f;
        analogGlitch.scanLineJitter = 0f;

        deathEffectOn = false;
    }

    public void DeathScreenEffect() {
        analogGlitch.enabled = true;
        analogGlitch.colorDrift = 0.5f;
        analogGlitch.scanLineJitter = 0.6f;

        deathEffectOn = true;
        CancelInvoke();
        Invoke("TurnOffScreenEffects", deathEffectDuration);
    }

}
