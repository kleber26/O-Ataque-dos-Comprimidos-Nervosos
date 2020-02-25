using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
//  # Color Management
    public Material DamageMaterial;
    public Renderer body, body_2;
    private Material body_2DefaultMaterial;
    private bool tookDamage;
    private float timeToChangeColor;
    private Material bodyDefaultMaterial;
    
//  # Audio and Visual Effects    
    public ParticleSystem explosionEffect;
    public List <AudioSource> scream;
    private AudioManager audioManager;
    
//  # General
    public bool boss;
    public bool special;
    public float health = 100f;
    public RectTransform healthBar;
    public float explosionDamage = 20f;
    
//  # Kill Counter    
    private TextMeshProUGUI killCounter;
    private TextMeshProUGUI explosionsCounter;
    
// # GameFlow - Kill boss
    private GameFlow eventSystem;
   
    private void Start() {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<GameFlow>();
        bodyDefaultMaterial = body.GetComponent<Renderer>().material;
        body_2DefaultMaterial = body_2.GetComponent<Renderer>().material;
        killCounter = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        explosionsCounter = GameObject.FindGameObjectWithTag("Exploded").GetComponent<TextMeshProUGUI>();
        ChangeEnemyColor();
    }

    private void Update() {
        ChangeColorOnDamage();
    }

    public void TakeDamage(float amount) {
        health -= amount;
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        DamageSound();
        AddDamageColor();
        
        if (health <= 0f) {
            IncreaseKillCounter();
            Die(true);
        }
    }

    public void Die(bool killedByPlayeer) {
        SpecialGhostsKilled special = gameObject.GetComponent<SpecialGhostsKilled>();
        if (special != null && killedByPlayeer) special.UpdateSpecialGhostsScore();

        if (boss) {
            eventSystem.CallWinScreen();
            StartCoroutine(BossExplosionEffect());
        } else {
            Instantiate(explosionEffect, gameObject.transform.position, transform.rotation);
            Destroy(gameObject, 0.1f);
        }        
    }

    private void IncreaseKillCounter() {
        killCounter.text = (int.Parse(killCounter.text) + 1).ToString();
    }
    
    private void IncreaseExplosionsCounter() {
        explosionsCounter.text = (int.Parse(explosionsCounter.text) + 1).ToString();
    }

    private void AddDamageColor() {
        body.material = DamageMaterial;
        body_2.material = DamageMaterial;
        tookDamage = true;
        timeToChangeColor = Time.time;
    }

    private void ChangeColorOnDamage() {
        if (CheckTimeToChangeDefaultColor()) {
            ChangeEnemyColor();
        }
    }
    
    private bool CheckTimeToChangeDefaultColor() {
        if (tookDamage && (Time.time - timeToChangeColor) > 0.2f) {
            tookDamage = false;
            return true;
        }
        return false;
    }
    
    private void ChangeEnemyColor() {
        body.material = bodyDefaultMaterial;
        body_2.material = body_2DefaultMaterial;
    }

    private void DamageSound() {
        int pos = Random.Range(0, 3);
        scream[pos].gameObject.active = true;
        scream[pos].Play();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerHealth player = other.transform.GetComponent<PlayerHealth>();
            player.TakeDamage(explosionDamage);

            IncreaseExplosionsCounter();
            if (!boss) Die(false);
        }
    }

    private IEnumerator BossExplosionEffect() {
        audioManager.PlayExplosionSound(3);

        ParticleSystem PS1 = Instantiate(explosionEffect, gameObject.transform.position, transform.rotation);
        PS1.transform.localScale = new Vector3(7f,7f,7f);
        yield return new WaitForSeconds(0.3f);
        
        audioManager.PlayExplosionSound(2);
        ParticleSystem PS2 = Instantiate(explosionEffect, gameObject.transform.position, transform.rotation);
        PS2.transform.localScale = new Vector3(25f,25f,25f);
        yield return new WaitForSeconds(0.5f);
        
        ParticleSystem PS3 = Instantiate(explosionEffect, gameObject.transform.position, transform.rotation);
        PS3.transform.localScale = new Vector3(15f,15f,15f);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject, 0.1f);
    }
}