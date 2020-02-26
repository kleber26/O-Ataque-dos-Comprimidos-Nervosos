using System.Collections;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour {
    
    public Rigidbody rigidPlayer;
    public Transform platform;
    public float speed = 5f;
    public AudioSource audioSource;
    public bool avoidPlayerStayOnPillar;
    public float timeToRespawn = 6f;
    
    public GameObject platformWithMaterial;
    private Color defaultColor;
    
    public int maxJumpsAllowed = 3;
    public int timesJumped = 0;
    private float lastTimeJumped = 0f;
    private Vector3 defaultPosition;
    
    private void Start() {
        avoidPlayerStayOnPillar = false;
        defaultColor = platformWithMaterial.GetComponent<Renderer>().materials[0].GetColor("_EmissionColor");
    }

    void Update() {
        if (lastTimeJumped + Time.deltaTime > timeToRespawn) timesJumped = 0;

        if (timesJumped > 0) lastTimeJumped += Time.deltaTime;
        else lastTimeJumped = 0f; 
    }

    void OnCollisionEnter(Collision collidedWithThis) {
        if (collidedWithThis.gameObject.CompareTag("Player")) {
            if (defaultPosition == Vector3.zero) defaultPosition = transform.position;
            
            rigidPlayer.velocity = transform.up * speed;
            platform.Translate(new Vector3(0f, -1f, 0f));
            audioSource.Play();

            timesJumped += 1;
        }
    }
    
    void OnCollisionExit(Collision collidedWithThis) {        
        if (collidedWithThis.gameObject.CompareTag("Player")) {
            platform.Translate(new Vector3(0f, 1f, 0f));

            if (timesJumped >= maxJumpsAllowed) {
                platformWithMaterial.GetComponent<Renderer>().materials[0].SetColor("_EmissionColor", new Color(1f,0f, 0f, 1f));
                gameObject.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(FallingPlatform());
                timesJumped = 0;
            }
        }
    }
    
    private IEnumerator FallingPlatform() {
        avoidPlayerStayOnPillar = true;
        yield return new WaitForSeconds(timeToRespawn);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        platformWithMaterial.GetComponent<Renderer>().materials[0].SetColor("_EmissionColor", defaultColor);
        transform.position = defaultPosition;
        avoidPlayerStayOnPillar = false;
    }
}