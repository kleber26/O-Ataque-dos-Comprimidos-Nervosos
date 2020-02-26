using UnityEngine;

public class SuperShot : MonoBehaviour {
    
    private GameObject player;
    private GameObject superShotEffect;
    private AudioManager audioManager;
    public float superShotDamage;
    
    void Start() {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        superShotEffect = GameObject.FindGameObjectWithTag("SuperShotEffect");
    }

    private void OnTriggerEnter(Collider other) { 
        Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
        if (Gun.superShot) return;
        
        if (other.CompareTag("Player")) {
            Gun.superShotDamage = superShotDamage;
            superShotEffect.GetComponent<ParticleSystem>().Play();
            audioManager.PlayCollectSuperShotSound();
            Gun.superShot = true;
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerExit(Collider other) {
        Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>(), false);
    }
}
