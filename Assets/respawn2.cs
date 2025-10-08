using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPoint;

    [Header("Checkpoint Sound")]
    public AudioClip checkpointSound;   // Drag your sound here
    public float volume = 1f;
    private AudioSource audioSource;

    void Start()
    {
        // Default respawn point = starting position
        respawnPoint = transform.position;

        // Setup AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    public void SetCheckpoint(Vector3 newPoint)
    {
        respawnPoint = newPoint;

        // 🔊 Play checkpoint sound
        if (checkpointSound != null)
        {
            audioSource.PlayOneShot(checkpointSound, volume);
        }
    }

    public void Respawn()
    {
        transform.position = respawnPoint;

        // Optional: reset velocity if using Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }
}