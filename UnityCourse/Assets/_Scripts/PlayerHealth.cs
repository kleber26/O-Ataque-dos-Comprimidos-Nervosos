using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public CanvasGroup myCG;
    public CanvasGroup healCanvasGroup;
    public AudioSource PlayerDeathSound;
    public AudioSource PlayerDamageSound;
    public RectTransform healthBar;
    public GameObject winScreen;
    public AudioSource healSound;
    public static bool playerDead;
    public static bool immortal;
    
    private float health = 100f;
    private bool flashDamage, flashHeal;
    private float flashTime = 0.05f;
    
    void Update () {
        CheckDamage();
        CheckHeal();
    }

    public void HealPlayer(float heal) {
        health = heal;
        UpdateHealthBar();
        healSound.Play();
        healCanvasGroup.alpha = 0.7f;
        flashHeal = true;
    }
    
    public float CurrentHealth() {
        return health;
    }

    public void FlashDamage () {
        flashDamage = true;
        myCG.alpha = 1;
    }
    
    public void TakeDamage(float amount) {
        if (immortal) return;
        
        if (!PlayerDamageSound.isPlaying) PlayerDamageSound.Play();
        health -= amount;
        UpdateHealthBar();
        
        if (health <= 0f) Die();
        else FlashDamage();
    }
    
    private void CheckDamage() {
        if (flashDamage) {
            myCG.alpha = myCG.alpha - (Time.deltaTime + flashTime);
            if (myCG.alpha <= 0) {
                myCG.alpha = 0;
                flashDamage = false;
            }
        }
    }

    private void CheckHeal() {
        if (flashHeal) {
            healCanvasGroup.alpha = healCanvasGroup.alpha - (Time.deltaTime + flashTime);
            if (healCanvasGroup.alpha <= 0) {
                healCanvasGroup.alpha = 0;
                flashHeal = false;
            }
        }
    }

    void Die() {
        Gun.superShot = false;
        PlayerDeathSound.gameObject.active = true;
        PlayerDeathSound.Play();
        playerDead = true;
        gameObject.SetActiveRecursively(false);
    }

    private void UpdateHealthBar() {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

}