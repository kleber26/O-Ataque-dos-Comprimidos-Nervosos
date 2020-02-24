using UnityEngine;

public class BouncyPlatform : MonoBehaviour {
    
    public Rigidbody rigidPlayer;
    public Transform platform;
    public float speed = 5f;
    public AudioSource audioSource; 
    
    void OnCollisionEnter(Collision collidedWithThis) {
        if (collidedWithThis.gameObject.CompareTag("Player")) {
            rigidPlayer.velocity = transform.up * speed;
            platform.transform.Translate(new Vector3(0f, -1f, 0f));
            audioSource.Play();
        }
    }
    
    void OnCollisionExit(Collision collidedWithThis) {
        if (collidedWithThis.gameObject.CompareTag("Player")) {
            platform.transform.Translate(new Vector3(0f, 1f, 0f));
        }
    }
}
