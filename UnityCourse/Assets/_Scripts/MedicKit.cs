using UnityEngine;

public class MedicKit : MonoBehaviour {
    
    private GameObject player;
    private PlayerHealth playerHealth;
    
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other) { 
        if (playerHealth.CurrentHealth() == 100f) return;
        
        if (other.CompareTag("Player")) {
            playerHealth.HealPlayer(100f);
            Destroy(gameObject);
        }
    }
}
