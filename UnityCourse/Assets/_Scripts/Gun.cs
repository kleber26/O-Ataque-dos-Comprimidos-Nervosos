using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 20;
    public float range = 200f;
    public float impactForce = 1000f;
      
    public Camera fpsCam;
    public Animator animator;
    public List <AudioSource> shot;
    
    private GameObject flash;
    public ParticleSystem impact;
    private float nextTimetoFire = 0f;

    void Awake() {
        flash = GameObject.FindGameObjectWithTag("Flash");
    }

    void Update() {
        if (animator.GetBool("isAiming")) ShootCondition();
    }

    void ShootCondition() {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimetoFire) {
            nextTimetoFire = Time.time ;
            nextTimetoFire += 0.5f;
            animator.CrossFadeInFixedTime("Firing", .2f);
            Shoot();
        } else if (Input.GetMouseButtonUp(0)) animator.ResetTrigger("Fire");
    }

    void Shoot() {
        ShootSound();
        flash.GetComponent<ParticleSystem>().Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {

            Debug.Log("Target shot: " + hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (target != null) target.TakeDamage(damage);
            if (enemy != null) enemy.TakeDamage(damage);

            if (hit.rigidbody != null) {
                hit.rigidbody.AddForceAtPosition(fpsCam.transform.TransformDirection(new Vector3(0f, 0f, impactForce)), hit.point );
            }

            ParticleSystem go = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(go, 2f);
        }
    }

    void ShootSound() {
        int pos = Random.Range(0, 2);
        shot[pos].gameObject.active = true;
        shot[pos].Play();
    }
}
