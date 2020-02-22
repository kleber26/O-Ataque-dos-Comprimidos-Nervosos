using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawner : MonoBehaviour {

    public GameObject spawnee;
    public bool stopSpawning;
    public float spawnTime;
    public float spawnDelay;
    public bool boss;
    private GameObject trig;
    private GameObject bossTrig;
    private bool bossAlreeadyActivated;
    // Start is called before the first frame update

    private void Update() {
        trig = GameObject.FindGameObjectWithTag("bosstrig");
    }

    void Start() {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject() {
        if (!boss) {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < 4) Instantiate(spawnee, transform.position, transform.rotation);
            if (stopSpawning) CancelInvoke("SpawnObject");
        } else {
            if (trig != null && !bossAlreeadyActivated) {
                SpawnBoss();
                bossAlreeadyActivated = true;
            } 
        }
    }

    private void SpawnBoss() {
        Instantiate(spawnee, transform.position, transform.rotation);
    }

}