using UnityEngine;

public class PauseMenu : MonoBehaviour {

    private bool gameIsPaused;
    public GameObject pauseMenuUI;
    private MainMenuInGame load;
    private GameFlow eventSystem;

    private void Start() {
        load = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<MainMenuInGame>();
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<GameFlow>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P) && !PlayerHealth.playerDead && !GameFlow.winScreenActivated) {
            if (gameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume() {
        eventSystem.ToggleLowFilterMusicBackground();
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    
    private void Pause() {
        eventSystem.ToggleLowFilterMusicBackground();
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu() {
        eventSystem.ToggleLowFilterMusicBackground();
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
        load.GameMenu();
        
    }
    
    public void QuitGame() {
        Debug.Log("Quitting game. It does not pause the Unity Play.");
        Application.Quit();
    }
}
