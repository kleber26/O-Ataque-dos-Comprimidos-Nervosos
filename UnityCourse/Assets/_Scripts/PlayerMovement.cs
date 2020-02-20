 using System.Collections;
using System.Collections.Generic;
 using UnityEditor;
 using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;

    private float speed = 3f;
    private float gravity = -13f;
    public float jumpHeight = 0.6f;

    public Transform groundCheck;
    public float groundDistance =0.01f;
    public LayerMask groundMask;
    
    private Vector3 velocity;
    public bool isGrounded;
    public Vector3 cubeGizmos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        isGrounded = Physics.CheckBox(groundCheck.position, cubeGizmos, new Quaternion(1,1,1,1), groundMask);            //CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        if(Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Jump Action
        }

        if(Input.GetKey(KeyCode.LeftShift)) {
            speed = 5f;
        }
        
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            speed = 3f;
        } 
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        //Gizmos.DrawSphere(groundCheck.position, groundDistance);
        Gizmos.DrawCube(groundCheck.position, cubeGizmos);
    }
}
