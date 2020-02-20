using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour {

    public CanvasGroup myCG;
    public AudioSource PlayerDeathSound;
    public AudioSource PlayerDamageSound;
    public RectTransform healthBar;
    
    private float health = 100f;
    private bool flash;
    
    void Update () {
        CheckDamage();
    }

    private void CheckDamage() {
        if (flash) {
            myCG.alpha = myCG.alpha - (Time.deltaTime + 0.05f);
            if (myCG.alpha <= 0) {
                myCG.alpha = 0;
                flash = false;
            }
        }
    }

    public void FlashDamage () {
        flash = true;
        myCG.alpha = 1;
    }
    
    public void TakeDamage(float amount) {
        FlashDamage();
        PlayerDamageSound.Play();
        health -= amount;
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        
        if (health <= 0f) {
            Die();
        }
    }

    void Die() {
        PlayerDeathSound.gameObject.active = true;
        PlayerDeathSound.Play();
        gameObject.active = false;
    }

}