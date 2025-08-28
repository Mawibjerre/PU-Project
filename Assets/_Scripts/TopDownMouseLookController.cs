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
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashCooldown = 1f;
    bool isDashing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }



    // Update is called once per frame
    void Update()
    {

        if (isDashing)
        {
            return;
        }

        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        moveDir = moveDir.normalized * speed;
        mousePos = Input.mousePosition;
        Vector2 screenPos = cam.WorldToScreenPoint(transform.position);
        Vector2 mouseDistance = mousePos - screenPos;
        angle = Mathf.Atan2(mouseDistance.y, mouseDistance.x) * Mathf.Rad2Deg;

        if(Input.GetKeyDown(KeyCode.Space))
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
    
    private IEnumerator Dash()
    {
        isDashing = true;
        rb.linearVelocity = new Vector2(moveDir.x * dashSpeed, moveDir.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }
}

