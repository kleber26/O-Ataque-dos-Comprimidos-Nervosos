using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour {

    public GameObject restartScreen;
    public GameObject backgroundMusic;
    public GameObject player;
    
    // Update is called once per frame
    void Update() {
        if (!player.active) CallRestartCanvas();
    }
    
    void CallRestartCanvas() {
        restartScreen.active = true;
        backgroundMusic.GetComponent<AudioLowPassFilter>().enabled = true;
        if (Input.GetKeyDown(KeyCode.Return)) RestartGame();
        if (Input.GetKeyDown(KeyCode.Q)) GameMenu();
    }

    void RestartGame() {
        Application.LoadLevel(1);
        restartScreen.active = false;
        backgroundMusic.GetComponent<AudioLowPassFilter>().enabled = false;
    }
    
    void GameMenu() {
        Application.LoadLevel(0);
        restartScreen.active = false;
        backgroundMusic.GetComponent<AudioLowPassFilter>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
