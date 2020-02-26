using System.Collections;
using UnityEngine;

public class GameFlow : MonoBehaviour {

    public Timer timer;
    public GameObject winScreen; 
    public GameObject restartScreen;
    public GameObject backgroundMusic;
    public GameObject levelLoader;
    public CameraShake cameraShake;
    public static bool winScreenActivated;
    
    private AudioManager audioManager;
    private AudioLowPassFilter lowFilterMusic;
    private MainMenuInGame load;
    private float defaultBackgroundVolume = 0.613f;
    private bool keepFadingIn, keepFadingOut;
    private GameObject[] invisibleWalls;
    
    void Start() {
        Timer.stopTimer = false;
        winScreenActivated = false;
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        invisibleWalls = GameObject.FindGameObjectsWithTag("InvisibleWalls");
        StartCoroutine(DisableInvisibleWalls());
        levelLoader.active = true;
        PlayerHealth.playerDead = false;
        lowFilterMusic = backgroundMusic.GetComponent<AudioLowPassFilter>();
        load = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<MainMenuInGame>();
    }
    
    void Update() {
        if (PlayerHealth.playerDead) CallRestartCanvas();
    }

    public static void ToggleWinScreen() {
        winScreenActivated = true;
    }
    
    public void CallWinScreen() {
        Timer.stopTimer = true;
        StartCoroutine(KillBossEffect());
        ToggleWinScreen();
    }

    public void FadeOutBackgroudMusic() {
        StartCoroutine(SoundFadeOut(backgroundMusic.GetComponent<AudioSource>(), 0.06f));
    }

    public void FadeInBackgroudMusic() {
        StartCoroutine(SoundFadeIn(backgroundMusic.GetComponent<AudioSource>(), 0.06f, defaultBackgroundVolume));
    }
    
    void CallRestartCanvas() {
        Timer.stopTimer = true;
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

    public void ToggleLowFilterMusicBackground() {
        lowFilterMusic.enabled = !lowFilterMusic.enabled;
    }

    public void CameraShake(float duration, float magnitude) {
        StartCoroutine(cameraShake.Shake(duration, magnitude));
    }

    public void CallSlowMotion(float time) {
        if (Enemy.focusSlowMotionOnBoss) {
            return;
        }
        StartCoroutine(SlowMotion(time));
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

    private IEnumerator KillBossEffect() {
        CameraShake(0.85f, 0.2f);
        audioManager.ToggleAudioEchoFilter();
        audioManager.PlayBossKilledSound();
        Time.timeScale = 0.25f;
        lowFilterMusic.enabled = true;
        yield return new WaitForSeconds(0.85f);
        audioManager.ToggleAudioEchoFilter();
        lowFilterMusic.enabled = false;
        Time.timeScale = 1f;
        winScreen.active = true;
        audioManager.PlayExplosionSound(3);
    }

    private IEnumerator SlowMotion(float time) {
        audioManager.ToggleAudioEchoFilter();
        Time.timeScale = 0.25f;
        lowFilterMusic.enabled = true;
        yield return new WaitForSeconds(time);
        audioManager.ToggleAudioEchoFilter();
        lowFilterMusic.enabled = false;
        Time.timeScale = 1f;
    }
}