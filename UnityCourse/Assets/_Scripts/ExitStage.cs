using TMPro;
using UnityEngine;

public class ExitStage : MonoBehaviour {
    
    public TextMeshProUGUI panelsLeft;
    public TextMeshProUGUI jorge;
    public TextMeshProUGUI brazuka;
    public TextMeshProUGUI zoio;
    public TextMeshProUGUI quem;
    
    public GameObject gate;
    public GameObject glowRED;
    public GameObject glowBLUE;

    void Update() {
        bool currentPanels   = int.Parse(panelsLeft.text.Substring(0, 1)) == 5;
        
        bool jorgeKilleds    = int.Parse(jorge.text.Substring(0, 1))      >= 1;
        bool brazukaKilleds  = int.Parse(brazuka.text.Substring(0, 1))    >= 1;
        bool zoioKilleds     = int.Parse(zoio.text.Substring(0, 1))       >= 1;
        bool quemKilleds     = int.Parse(quem.text.Substring(0, 1))       >= 1;
        
        if (currentPanels && jorgeKilleds && brazukaKilleds && zoioKilleds && quemKilleds) {
            if (gate.active) {
                gate.active = false;
                glowRED.active = false;
                glowBLUE.active = true;
            }
        }
    }
}
