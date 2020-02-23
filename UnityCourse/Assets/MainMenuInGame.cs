using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuInGame : MonoBehaviour {
    public Animator transition;
    public float transitionTime = 1f;
    private GameFlow eventSystem;

    private void Start() {
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<GameFlow>();
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

    IEnumerator LoadLevel(int levelIndex) {
        // Play Animation
        transition.SetTrigger("Start");
        
        // Wait
        yield return new WaitForSeconds(transitionTime);
        
        // Load Scene
        SceneManager.LoadScene(levelIndex);
    }
}
