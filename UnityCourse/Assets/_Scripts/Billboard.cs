using UnityEngine;

public class Billboard : MonoBehaviour {

    public bool inverted;
    void Update() {
        if (inverted) transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        else transform.LookAt(Camera.main.transform);
    }
}
