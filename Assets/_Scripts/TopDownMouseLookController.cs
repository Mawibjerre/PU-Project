using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TopDownMouseLookController : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;

    Vector2 moveDir;
    Vector2 mousePos;
    float angle;
    Camera cam;

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCooldown = 1f;

    [Header("Audio Settings")]
    public AudioClip dashSound;      // Drag your dash sound here
    public float dashVolume = 1f;    // Adjust volume
    private AudioSource audioSource;

    private bool isDashing;
    private bool isOnCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        // Add or get AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (isDashing) return;

        // Movement input
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        moveDir = moveDir.normalized * speed;

        // Aim with mouse
        mousePos = Input.mousePosition;
        Vector2 screenPos = cam.WorldToScreenPoint(transform.position);
        Vector2 mouseDistance = mousePos - screenPos;
        angle = Mathf.Atan2(mouseDistance.y, mouseDistance.x) * Mathf.Rad2Deg;

        // Dash input
        if (Input.GetKeyDown(KeyCode.Space) && !isOnCooldown)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.linearVelocity = moveDir;
    }

    public IEnumerator Dash()
    {
        isDashing = true;
        isOnCooldown = true;

        // 🔊 Play dash sound
        if (dashSound != null)
        {
            audioSource.PlayOneShot(dashSound, dashVolume);
        }

        // Apply dash velocity
        rb.linearVelocity = new Vector2(moveDir.x * dashSpeed, moveDir.y * dashSpeed);

        // Wait dash duration
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        // Wait cooldown
        yield return new WaitForSeconds(dashCooldown);
        isOnCooldown = false;
    }
}