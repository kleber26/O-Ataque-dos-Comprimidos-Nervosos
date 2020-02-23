using UnityEngine;

public class WinScreen : MonoBehaviour {

    private MainMenuInGame load;

    void Start() {
        load = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<MainMenuInGame>();
    }
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) RestartGame();
        if (Input.GetKeyDown(KeyCode.Q)) GameMenu();
    }

    void RestartGame() {
        load.PlayGame();
    }
    
    void GameMenu() {
        load.GameMenu();
        Cursor.lockState = CursorLockMode.Confined;
    }
    
}