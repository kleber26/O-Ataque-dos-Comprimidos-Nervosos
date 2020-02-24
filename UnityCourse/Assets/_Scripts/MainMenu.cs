﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Animator transition;
    public float transitionTime = 1f;
    public GameObject commandsPopup;
    private MenuFlow eventSystem;

    private void Start() {
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<MenuFlow>();
    }

    public void PlayGame() {
        eventSystem.FadeOutBackgroudMusic();
        StartCoroutine(LoadLevel(1));
    }

    public void GameMenu() {
        eventSystem.FadeOutBackgroudMusic();
        StartCoroutine(LoadLevel(0));
    }
    
    public void QuitGame() {
        Debug.Log("Quitting game. It does not pause the Unity Play.");
        Application.Quit();
    }

    public void ToggleCommandsPopup() {
        commandsPopup.active = !commandsPopup.active;
    }

    IEnumerator LoadLevel(int levelIndex) {
        // Play Animation
        transition.SetTrigger("Start");
        
        // Wait
        yield return new WaitForSeconds(transitionTime);
        
        // Load Scene
        SceneManager.LoadScene(levelIndex);
    }
}
