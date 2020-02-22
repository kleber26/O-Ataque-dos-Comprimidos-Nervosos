using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSmallDoorUp : MonoBehaviour {

    public GameObject door;
    public float x, y, z = 0f;
    public bool openBoss;
    public GameObject bossTrig;
    
    private void OnTriggerEnter(Collider other) {
        door.transform.position += new Vector3(-x, -y, -z) * Time.deltaTime;
        if (openBoss) {
            if (!bossTrig.active) {
                bossTrig.active = true;
            }
        }
    }
    
    private void OnTriggerExit(Collider other) {
        door.transform.position += new Vector3(x, y, z) * Time.deltaTime;
    }

}
