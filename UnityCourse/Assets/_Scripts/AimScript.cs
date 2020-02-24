using UnityEngine;

public class AimScript : MonoBehaviour {

    public Transform target;
    public Vector3 offset;

    private Animator anim;
    private Transform chest;
    
    void Start() {
        anim = GetComponent<Animator>();
        chest = anim.GetBoneTransform(HumanBodyBones.Chest);
    }

    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(target.transform.position, target.transform.forward, out hit, 200)) {
            chest.LookAt(hit.point);
            chest.rotation = chest.rotation * Quaternion.Euler(offset);
        }
    }
}
