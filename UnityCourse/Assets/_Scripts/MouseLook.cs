using UnityEngine;

public class MouseLook : MonoBehaviour {

    public float mouseSebsitivity = 150f;
    public Transform playerBody;
    private float xRotation = 0f;
    
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSebsitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSebsitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -50f, 80f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
