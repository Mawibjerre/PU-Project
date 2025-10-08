using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CoinCollect : MonoBehaviour
{
    [Header("Sound Settings")]
    public AudioClip collectSound;   // Drag your coin sound here in Inspector
    public float volume = 1f;        // Adjust volume if needed

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Play the sound at the coin's position
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);
            }

            // Destroy the coin after collection
            Destroy(gameObject);
        }
    }
}