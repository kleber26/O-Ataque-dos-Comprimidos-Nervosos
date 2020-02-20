using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawner : MonoBehaviour {

    public GameObject spawnee;
    public bool stopSpawning;
    public float spawnTime;
    public float spawnDelay;
    
    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject() {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 4) Instantiate(spawnee, transform.position, transform.rotation); 
        if (stopSpawning) CancelInvoke("SpawnObject");
    }
}
