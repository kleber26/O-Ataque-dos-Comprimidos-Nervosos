using UnityEngine;

public class PushPlayerOff : MonoBehaviour {

    public GameObject bouncyPlatform;
    private Rigidbody rigidPlayer;

    void Start() {
        rigidPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && bouncyPlatform.GetComponent<BouncyPlatform>().avoidPlayerStayOnPillar) {
            if (other.CompareTag("Player")) rigidPlayer.velocity = -transform.forward * 10;
        }
    }
}
