using TMPro;
using UnityEngine;

public class Collectable : MonoBehaviour {
    public GameObject activated;
    public GameObject deactivated;
    public AudioSource sound;
    public TextMeshProUGUI panelsLeft;
    
    private void UpdatePanelsLeft() {
//        string currentPanels = panelsLeft.text.;
        int currentPanels = int.Parse(panelsLeft.text.Substring(0, 1));
        panelsLeft.text = (currentPanels + 1) + "/5";
        
        if (currentPanels + 1 == 5) panelsLeft.color = Color.green;
    }

    private void OnCollisionEnter(Collision other) {
        if (!activated.active) {
            if (other.gameObject.CompareTag("Player")) {
                sound.Play();
                deactivated.active = false;
                activated.active = true;

                UpdatePanelsLeft();
            }
        }
    }
}
