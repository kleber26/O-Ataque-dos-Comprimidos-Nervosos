using System.Collections;
using UnityEngine;

public class GameFlow : MonoBehaviour {

    public GameObject winScreen; 
    public GameObject restartScreen;
    public GameObject backgroundMusic;
    private AudioLowPassFilter lowFilterMusic;
    private MainMenuInGame load;
    private float defaultBackgroundVolue = 0.613f;
    private bool keepFadingIn, keepFadingOut;
    private GameObject[] invisibleWalls;
    public static bool winScreenActivated;
    
    void Start() {
        winScreenActivated = false;
        invisibleWalls = GameObject.FindGameObjectsWithTag("InvisibleWalls");
        StartCoroutine(DisableInvisibleWalls());
        PlayerHealth.playerDead = false;
        lowFilterMusic = backgroundMusic.GetComponent<AudioLowPassFilter>();
        load = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<MainMenuInGame>();
    }
    
    void Update() {
        if (PlayerHealth.playerDead) CallRestartCanvas();
        else ToggleLowFilterMusicBackground(PauseMenu.gameIsPaused);

        WinScreen();
    }

    public static void ActivateWinScreen() {
        winScreenActivated = true;
    }
    
    private void WinScreen() {
        if (winScreenActivated) winScreen.active = true;
        else winScreen.active = false;
    }

    public void FadeOutBackgroudMusic() {
        StartCoroutine(SoundFadeOut(backgroundMusic.GetComponent<AudioSource>(), 0.06f));
    }

    public void FadeInBackgroudMusic() {
        StartCoroutine(SoundFadeIn(backgroundMusic.GetComponent<AudioSource>(), 0.06f, defaultBackgroundVolue));
    }
    
    void CallRestartCanvas() {
        restartScreen.active = true;
        lowFilterMusic.enabled = true;
        Time.timeScale = 0f;
        if (Input.GetKeyDown(KeyCode.Return)) RestartGame();
        if (Input.GetKeyDown(KeyCode.Q)) GameMenu();
    }

    void RestartGame() {
        ResetDefaultValuesOnChangeScene();
        StartCoroutine(SoundFadeOut(backgroundMusic.GetComponent<AudioSource>(), 0.06f));
        load.PlayGame();
    }
    
    void GameMenu() {
        ResetDefaultValuesOnChangeScene();
        Cursor.lockState = CursorLockMode.Confined;
        StartCoroutine(SoundFadeOut(backgroundMusic.GetComponent<AudioSource>(), 0.06f));
        load.GameMenu();
    }

    void ResetDefaultValuesOnChangeScene() {
        PlayerHealth.playerDead = false;
        Time.timeScale = 1f;
        restartScreen.active = false;
        lowFilterMusic.enabled = false;
    }

    void ToggleLowFilterMusicBackground(bool paused) {
        lowFilterMusic.enabled = paused;
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

    private IEnumerator DisableInvisibleWalls() {
        foreach (var invisible in invisibleWalls) {
            invisible.active = false;
            yield return null;
        }
    }
}