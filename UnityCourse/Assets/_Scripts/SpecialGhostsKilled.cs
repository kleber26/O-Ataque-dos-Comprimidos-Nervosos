using TMPro;
using UnityEngine;

public class SpecialGhostsKilled : MonoBehaviour{
    
    public TextMeshProUGUI name;
    private string enemyName;
    private GameObject[] jorgeField;
    private GameObject[] brazukaField;
    private GameObject[] zoioField;
    private GameObject[] quemField;
    private int currentKills;
    
    private void Start() {
        enemyName = name.text + ":";
        enemyName = enemyName.Substring(0, enemyName.IndexOf(":")).ToLower();
        
        jorgeField = GameObject.FindGameObjectsWithTag("JorgeField");
        brazukaField = GameObject.FindGameObjectsWithTag("BrazukaField");
        zoioField = GameObject.FindGameObjectsWithTag("ZoioField");
        quemField = GameObject.FindGameObjectsWithTag("???Field");
    }

    public void UpdateSpecialGhostsScore() {
        switch (enemyName) {
            case "jorge":
                UpdateValues(jorgeField[0], false);
                UpdateValues(jorgeField[1], true);
                break;
            case "brazuka":
                UpdateValues(brazukaField[0], false);
                UpdateValues(brazukaField[1], true);
                break;
            case "zoio":
                UpdateValues(zoioField[0], false);
                UpdateValues(zoioField[1], true);
                break;
            case "???":
                UpdateValues(quemField[0], false);
                UpdateValues(quemField[1], true);
                break; 
        }
    }

    private void UpdateValues(GameObject enemy, bool changeColor) {
        TextMeshProUGUI text = enemy.GetComponent<TextMeshProUGUI>();
        currentKills = int.Parse(text.text.Substring(0, 1));
        text.text = (currentKills + 1) + "/1";
        if (changeColor) text.color = Color.green;
    }
}