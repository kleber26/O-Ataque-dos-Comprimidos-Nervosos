using TMPro;
using UnityEngine;

public class SpecialGhostsKilled : MonoBehaviour{
        
    public TextMeshProUGUI name;
    private string enemyName;
    private TextMeshProUGUI jorgeField;
    private TextMeshProUGUI brazukaField;
    private TextMeshProUGUI zoioField;
    private TextMeshProUGUI quemField;
    private int currentKills;
    
    private void Start() {
        enemyName = name.text + ":";
        enemyName = enemyName.Substring(0, enemyName.IndexOf(":")).ToLower();
        
        jorgeField = GameObject.FindGameObjectWithTag("JorgeField").GetComponent<TextMeshProUGUI>();
        brazukaField = GameObject.FindGameObjectWithTag("BrazukaField").GetComponent<TextMeshProUGUI>();
        zoioField = GameObject.FindGameObjectWithTag("ZoioField").GetComponent<TextMeshProUGUI>();
        quemField = GameObject.FindGameObjectWithTag("???Field").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateSpecialGhostsScore() {
        switch (enemyName) {
            case "jorge":
                currentKills = int.Parse(jorgeField.text.Substring(0, 1));
                jorgeField.text = (currentKills + 1) + "/1";
                jorgeField.color = Color.green;
                break;
            case "brazuka":
                currentKills = int.Parse(brazukaField.text.Substring(0, 1));
                brazukaField.text = (currentKills + 1) + "/1";
                brazukaField.color = Color.green;
                break;
            case "zoio":
                currentKills = int.Parse(zoioField.text.Substring(0, 1));
                zoioField.text = (currentKills + 1) + "/1";
                zoioField.color = Color.green;
                break;
            case "???":
                currentKills = int.Parse(quemField.text.Substring(0, 1));
                quemField.text = (currentKills + 1) + "/1";
                quemField.color = Color.green;
                break; 
        }
    }
}