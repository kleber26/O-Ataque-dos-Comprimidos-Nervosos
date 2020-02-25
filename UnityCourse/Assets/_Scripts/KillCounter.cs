using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour {
    
    public TextMeshProUGUI killCounter;
    public TextMeshProUGUI killCounterBlack;
    private string starterKillCount = "0";
    public TextMeshProUGUI explodedGhosts;
    private string starterHealth = "0";

    public AudioSource enemyExplosionSound;
    public AudioSource enemyDeathSound;

    void Update() {            
        if (!killCounter.text.Equals(starterKillCount)) {
            killCounterBlack.text = "<color=black>" + killCounter.text;
            starterKillCount = killCounter.text;
            EnemyDeathSound();
        }
        
        if (!explodedGhosts.text.Equals(starterHealth)) {
            starterHealth = explodedGhosts.text;
            EnemyDeathSound();
        }
    }
    
    private void EnemyDeathSound() {
        enemyExplosionSound.Play();
        enemyDeathSound.Play();
    }
}
