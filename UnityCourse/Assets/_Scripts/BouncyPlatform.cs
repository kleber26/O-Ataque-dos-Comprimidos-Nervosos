using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{

    public float speed = 5f;
    public Rigidbody rigidPlayer;
    private Rigidbody rigid;
    public Transform platform;
    
    void Start ()
    {
        rigid = transform.GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision collidedWithThis)
    {
        if (collidedWithThis.transform.tag == "Player")
        {
            rigidPlayer.velocity = transform.up * speed;
            platform.transform.Translate(new Vector3(0f, -1f, 0f));
        }
    }
    
    void OnCollisionExit(Collision collidedWithThis)
    {
        if (collidedWithThis.transform.tag == "Player")
        {
            platform.transform.Translate(new Vector3(0f, 1f, 0f));
        }
    }
}
