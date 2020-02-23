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
    
//  # General
    public bool boss;
    public float health = 100f;
    public RectTransform healthBar;
    public float explosionDamage = 20f;
    
//  # Kill Counter    
    private TextMeshProUGUI killCounter;
    private TextMeshProUGUI explosionsCounter;
   
    private void Start() {
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
            Die();
        }
    }

    private void Die() {
        SpecialGhostsKilled special = gameObject.GetComponent<SpecialGhostsKilled>();
        if (special != null) special.UpdateSpecialGhostsScore();

        Instantiate(explosionEffect, gameObject.transform.position, transform.rotation);        
        Destroy(gameObject, 0.1f);
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
            if (!boss) Die();
        }
    }
}