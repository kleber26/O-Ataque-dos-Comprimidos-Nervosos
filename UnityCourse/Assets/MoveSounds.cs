using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSounds : MonoBehaviour {

    public AudioSource run;

    void Run() {
        run.pitch = Random.Range(0.8f, 1.2f);
        run.Play();
    }

}
