using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour {

    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) RestartGame();
        if (Input.GetKeyDown(KeyCode.Q)) GameMenu();
    }

    void RestartGame() {
        Application.LoadLevel(1);
    }
    
    void GameMenu() {
        Application.LoadLevel(0);
        Cursor.lockState = CursorLockMode.Confined;
    }
    
}