using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioSource bossKilledSound;
    public AudioEchoFilter audioEchoFilter;
    public AudioSource explosionSound;
    
    public void PlayBossKilledSound() {
        bossKilledSound.Play();
    }

    public void ToggleAudioEchoFilter() {
        audioEchoFilter.enabled = !audioEchoFilter.enabled;
    }

    public void PlayExplosionSound(float pitch) {
        explosionSound.pitch = pitch;
        explosionSound.Play();
    }
}
