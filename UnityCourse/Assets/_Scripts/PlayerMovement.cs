using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;

    public float jumpHeight = 0.6f;
    public Transform groundCheck;
    public float groundDistance =0.01f;
    public LayerMask groundMask;
    public bool isGrounded;
    public Vector3 cubeGizmos;

    private float speed = 3f;
    private float gravity = -13f;
    private Vector3 velocity;
    
    void Update() {

        isGrounded = Physics.CheckBox(groundCheck.position, cubeGizmos, new Quaternion(1,1,1,1), groundMask);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        // Jump Action
        if (Input.GetButtonDown("Jump") && isGrounded) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
        if (Input.GetKey(KeyCode.LeftShift)) speed = 5f;
        if (Input.GetKeyUp(KeyCode.LeftShift)) speed = 3f;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, cubeGizmos);
    }
}
