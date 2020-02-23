using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool gameIsPaused;
    public GameObject pauseMenuUI;
    private MainMenuInGame load;

    private void Start() {
        load = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<MainMenuInGame>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P) && !PlayerHealth.playerDead) {
            if (gameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume() {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    
    private void Pause() {
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu() {
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
