using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    public GameObject body;
    public ParticleSystem explosionEffect;
    public List <AudioSource> scream;
    private TextMeshProUGUI killCounter;
    private TextMeshProUGUI explosionsCounter;
    
    private float health = 100f;
    public RectTransform healthBar;
    
    private Material[] materials;
    private float timeToChangeColor;
    private float explosionDamage = 20f;
   
    private void Start() {
        materials = body.GetComponent<Renderer>().materials;
        materials[0].color = Color.white;
        killCounter = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        explosionsCounter = GameObject.FindGameObjectWithTag("Exploded").GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        ChangeColorOnDamage();
        
    }

    public void TakeDamage(float amount) {
        health -= amount;
        DamageSound();
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        materials[0].color = Color.red;
        timeToChangeColor = Time.time;
            
        if (health <= 0f) {
            killCounter.text = (int.Parse(killCounter.text) + 1).ToString();
            Die();
        }
    }

    void Die() {
        Instantiate(explosionEffect, gameObject.transform.position, transform.rotation);        
        Destroy(gameObject, 0.1f);
    }

    private void ChangeColorOnDamage() {
        if (materials[0].color == Color.red && (Time.time - timeToChangeColor) > 0.2f) {
            materials[0].color = Color.white;
        }
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
            
            explosionsCounter.text = (int.Parse(explosionsCounter.text) + 1).ToString();
            Die();
        }   
    }
}