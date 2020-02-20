using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitStage : MonoBehaviour {
    
    public TextMeshProUGUI panelsLeft;
    public GameObject gate;
    public GameObject glowRED;
    public GameObject glowBLUE;

    void Update() {
        int currentPanels = int.Parse(panelsLeft.text.Substring(0, 1));
        
        if (currentPanels == 5) {
            if (gate.active) {
                gate.active = false;
                glowRED.active = false;
                glowBLUE.active = true;
            }
        }
    }
}
