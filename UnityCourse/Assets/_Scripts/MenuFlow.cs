using System.Collections;
using UnityEngine;

public class MenuFlow : MonoBehaviour {
    
    public GameObject levelLoader;
    public GameObject backgroundMusic;
    private float defaultBackgroundVolue = 0.708f;
    private bool keepFadingIn, keepFadingOut;
    
    private void Start() {
        levelLoader.active = true;
    }

    public void FadeOutBackgroudMusic() {
        StartCoroutine(SoundFadeOut(backgroundMusic.GetComponent<AudioSource>(), 0.06f));
    }

    public void FadeInBackgroudMusic() {
        StartCoroutine(SoundFadeIn(backgroundMusic.GetComponent<AudioSource>(), 0.06f, defaultBackgroundVolue));
    }
    
    private IEnumerator SoundFadeIn(AudioSource audio, float speed, float maxVolume) {
        keepFadingIn = true;
        keepFadingOut = false;

        audio.volume = 0f;
        float currentVolume = audio.volume;

        while (currentVolume < maxVolume && keepFadingIn) {
            currentVolume += speed;
            audio.volume = currentVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }
 
    private IEnumerator SoundFadeOut(AudioSource audio, float speed) {
        keepFadingIn = false;
        keepFadingOut = true;

        float currentVolume = audio.volume;

        while (currentVolume >= speed && keepFadingOut) {
            currentVolume -= speed;
            audio.volume = currentVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
