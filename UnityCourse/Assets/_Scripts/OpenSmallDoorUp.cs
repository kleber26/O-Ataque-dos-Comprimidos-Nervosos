using System.Collections;
using UnityEngine;

public class OpenSmallDoorUp : MonoBehaviour {

    public GameObject door;
    public float x, y, z;
    public bool openBoss;
    public GameObject bossTrig;
    
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;
        
        door.transform.position += new Vector3(-x, -y, -z) * Time.deltaTime;
        if (openBoss) {
            if (!bossTrig.active) {
                bossTrig.active = true;
            }
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Player")) return;
        
        door.transform.position += new Vector3(x, y, z) * Time.deltaTime;
    }
}
