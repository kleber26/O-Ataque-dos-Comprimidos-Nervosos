using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 20;
    public static float superShotDamage = 480f;
    public float range = 200f;
    public float impactForce = 1000f;
    public static bool superShot;
    
    public Camera fpsCam;
    public Animator animator;
    public List <AudioSource> shot;

    private GameFlow eventSystem;
    private AudioManager audioManager;
    private GameObject flash, superShotEffect;
    private bool oneHitKill;
    public ParticleSystem impact;
    private float nextTimetoFire = 0f;

    void Awake() {
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<GameFlow>();
        flash = GameObject.FindGameObjectWithTag("Flash");
        superShotEffect = GameObject.FindGameObjectWithTag("SuperShotEffect");
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Update() {
        if (animator.GetBool("isAiming")) ShootCondition();
    }

    public void ToggleOneHitKill() {
        oneHitKill = !oneHitKill;
        if (oneHitKill) damage = 10000;
        else damage = 20;
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
        float bonus = 0f;
        if (superShot) {
            bonus = superShotDamage;
            audioManager.StopCollectSuperShotSound();
            superShotEffect.GetComponent<ParticleSystem>().Stop();
        }

        ShootSound();
        flash.GetComponent<ParticleSystem>().Play();
        RaycastHit hit;
        
        // Allows casting the ray 1 meter from the camera view
        if (Physics.Raycast(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1.0f)), fpsCam.transform.forward, out hit, range)) {

            Debug.Log("Target shot: " + hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null) enemy.TakeDamage(damage + bonus);
            if (target != null) target.TakeDamage(damage);

            if (hit.rigidbody != null) {
                hit.rigidbody.AddForceAtPosition(fpsCam.transform.TransformDirection(new Vector3(0f, 0f, impactForce)), hit.point );
            }

            if (superShot) {
                eventSystem.CallSlowMotion(0.4f);
                superShot = false;
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