using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public IEnumerator Shake(float duration, float magnitude) {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0f;
        
        while (elapsed < duration) {
            float x = Random.Range(-0.5f, 0.5f) * magnitude;
            float y = Random.Range(-0.5f, 0.5f) * magnitude;
            
            transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}