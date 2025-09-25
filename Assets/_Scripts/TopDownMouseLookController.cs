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

    [Header("DashSettings")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCooldown = 1f;

    private bool isDashing;
    private bool isOnCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        // Hvis vi er midt i dash → stop input
        if (isDashing)
        {
            return;
        }

        // Movement input (skal altid virke, selv under cooldown)
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        moveDir = moveDir.normalized * speed;

        // Aim med mus
        mousePos = Input.mousePosition;
        Vector2 screenPos = cam.WorldToScreenPoint(transform.position);
        Vector2 mouseDistance = mousePos - screenPos;
        angle = Mathf.Atan2(mouseDistance.y, mouseDistance.x) * Mathf.Rad2Deg;

        // Dash input (kun hvis ikke på cooldown)
        if (Input.GetKeyDown(KeyCode.Space) && !isOnCooldown)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.linearVelocity = moveDir;
    }

    public IEnumerator Dash()
    {
        isDashing = true;
        isOnCooldown = true;

        // Giv karakteren fart i dash-retningen
        rb.linearVelocity = new Vector2(moveDir.x * dashSpeed, moveDir.y * dashSpeed);

        // Vent dash varighed
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        // Vent cooldown før næste dash
        yield return new WaitForSeconds(dashCooldown);
        isOnCooldown = false;
    }
}
