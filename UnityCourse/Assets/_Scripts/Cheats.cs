using UnityEngine;

public class Cheats : MonoBehaviour {

    public AudioSource sound;
    private string[] oneHitKill, noDamage, unlockGate;  
    private int oneHitKillINDEX, noDamageINDEX, unlockGateINDEX;
    private ExitStage gateScript;
    private Gun gunScript;

    void Start() {
        gateScript = GameObject.FindGameObjectWithTag("Exit").GetComponent<ExitStage>();
        gunScript = GameObject.FindGameObjectWithTag("Gun").GetComponent<Gun>();
        
        oneHitKill = new string[] { "o", "n", "e", "h", "i", "t", "0"};
        noDamage = new string[] { "i", "m", "m","o", "r", "t", "a", "l", "0" };
        unlockGate = new string[] { "u", "n", "l", "o", "c", "k", "0"};
    }
    
    void Update() {
        oneHitKill = CheckCheat(oneHitKill);
        noDamage = CheckCheat(noDamage);
        unlockGate = CheckCheat(unlockGate);
    }

    private string[] CheckCheat(string[] cheat) {
        int lenght = cheat.Length;
        int index = int.Parse(cheat[lenght - 1]);
        
        if (Input.anyKeyDown) {
            if (Input.GetKeyDown(cheat[index])) index++;
            else index = 0;
        }
     
        if (index == cheat.Length - 1) {
            ToggleCheat(cheat);
            index = 0;
        }

        cheat[lenght - 1] = index.ToString();
        return cheat;
    }

    private void ToggleCheat(string[] cheat) {
        string cheatName = string.Join("", cheat);

        switch (cheatName.Remove(cheatName.Length - 1)) {
            case "onehit":
                Debug.Log("Cheat Activated: Toggle one hit kill!");
                gunScript.ToggleOneHitKill();
                break;
            case "immortal":
                Debug.Log("Cheat Activated: Toggle immortality!");
                PlayerHealth.immortal = !PlayerHealth.immortal;
                break;
            case "unlock":
                Debug.Log("Cheat Activated: Toggle final gate!");
                gateScript.ToggleGate();
                break;
        }

        sound.Play();
    }

}