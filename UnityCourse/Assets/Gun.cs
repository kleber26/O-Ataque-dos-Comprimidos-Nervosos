using UnityEngine;
using UnityEngine.Networking;

public class Gun : MonoBehaviour {

    public float damage = 10;
    public float range = 200f;
    public float impactForce = 1000f;
      
    public Camera fpsCam;
    public Animator animator;

    private float nextTimetoFire = 0f;
    // Update is called once per frame
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
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {
            

//            else if (Input.GetMouseButtonUp(0)) animator.ResetTrigger("Fire");
//            animator.CrossFadeInFixedTime("Firing", .2f);


            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();

//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (target != null) {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null) {
//                hit.rigidbody.AddForce(-hit.normal * impactForce);
//                hit.rigidbody.AddExplosionForce(200f, transform.position, 10f);
                hit.rigidbody.AddForceAtPosition(fpsCam.transform.TransformDirection(new Vector3(0f, 0f, impactForce)), hit.point );

            }
        }
    } 
}
