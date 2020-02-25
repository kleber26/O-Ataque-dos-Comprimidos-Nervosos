using System.Collections;
using Invector.vCharacterController;
using UnityEngine;

public class MoveSounds : MonoBehaviour {

    public AudioSource run;
    private bool playerIsGrounded;
    private bool playerIsJumping;
    private bool canSoundLanding;
    
    private void Update() {
        if (!vThirdPersonMotor.playerIsGrounded) StartCoroutine(WaitPlayerGrounded());
    }

    void Run() {
        run.pitch = Random.Range(0.8f, 1.2f);
        run.Play();
    }
    
    void Landing() {
        run.pitch = 2f;
        run.Play();
    }

    private IEnumerator WaitPlayerGrounded() {
            yield return new WaitUntil(()=> vThirdPersonMotor.playerIsGrounded);
        Landing();
    }
}