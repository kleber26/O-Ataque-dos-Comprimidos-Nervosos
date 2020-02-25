using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawner : MonoBehaviour {

    public List<GameObject> spawnee;
    public float spawnTime;
    public float spawnDelay;
    public bool boss;
    private GameObject trig;
    
    private GameObject bossTrig;
    private bool bossAlreadyActivated;

//  # Manage times to spawn a special ghost
    public int maxTimesUntilSpecialGhost = 8;
    private int normalGhostsSpawned = 0;

    private void Update() {
        trig = GameObject.FindGameObjectWithTag("bosstrig");
    }

    void Start() {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject() {
        if (boss) {
            SpawnBoss();
            return;
        }
        
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 4) Instantiate(GenerateRandomEnemy(), transform.position, transform.rotation);
        if (GameFlow.winScreenActivated) StopSpawning();
    }

    private GameObject GenerateRandomEnemy() {
        int count = spawnee.Count;
        GameObject nextSpawn = spawnee[Random.Range(0, count)];
        
        CheckGhostTypeToBeSpawned(nextSpawn);
        if (normalGhostsSpawned > maxTimesUntilSpecialGhost) {
            normalGhostsSpawned = 0;
            return spawnee[count - 1];
        }
        
        return nextSpawn;
    }

    private void CheckGhostTypeToBeSpawned(GameObject nextSpawn) {
        if (nextSpawn.GetComponent<Enemy>().special) normalGhostsSpawned = 0;
        else normalGhostsSpawned += 1;
    }

    private void StopSpawning() {
        CancelInvoke("SpawnObject");
        StartCoroutine(DestroyAllEnemies(GameObject.FindGameObjectsWithTag("Enemy")));
    }

    private IEnumerator DestroyAllEnemies(GameObject[] enemies) {
        foreach (var obj in enemies) {
            Enemy enemy = obj.GetComponent<Enemy>();
            
            if (!enemy.boss) enemy.Die(false);
            yield return null;
        }
    }

    private void SpawnBoss() {
        if (bossAlreadyActivated) {
            CancelInvoke("SpawnBoss");
            return;
        }

        if (trig != null && !bossAlreadyActivated) {
            Instantiate(spawnee[0], transform.position, transform.rotation);
            bossAlreadyActivated = true;
        } 
    }
}