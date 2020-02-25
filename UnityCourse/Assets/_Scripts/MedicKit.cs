using UnityEngine;

public class MedicKit : MonoBehaviour {
    
    private GameObject player;
    private PlayerHealth playerHealth;
    
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other) { 
        Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
        if (playerHealth.CurrentHealth() == 100f) return;
        if (other.CompareTag("Player")) {
            playerHealth.HealPlayer(100f);
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerExit(Collider other) {
        Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>(), false);
    }
}
