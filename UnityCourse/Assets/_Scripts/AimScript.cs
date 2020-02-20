using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour {

    public Transform target;
    public Vector3 offset;

    private Animator anim;
    private Transform chest;
    
    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        chest = anim.GetBoneTransform(HumanBodyBones.Chest);
    }

    // Update is called once per frame
    void Update() {
        
        RaycastHit hit;
        if (Physics.Raycast(target.transform.position, target.transform.forward, out hit, 200)) {
            chest.LookAt(hit.point);
            chest.rotation = chest.rotation * Quaternion.Euler(offset);
        }
    }
}
