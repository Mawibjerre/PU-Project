using UnityEngine;

public class KillBlock : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector2 moveDirection = Vector2.right; // Direction to move (e.g., right, up)
    public float moveDistance = 5f;               // How far to move before looping
    public float speed = 2f;                      // Movement speed

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private bool movingForward = true;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + moveDirection.normalized * moveDistance;
    }

    void Update()
    {
        Vector2 currentTarget = movingForward ? targetPosition : startPosition;
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentTarget) < 0.01f)
        {
            movingForward = !movingForward;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Destroy player on contact
            Destroy(other.gameObject);

            // Optional: trigger respawn, game over, or damage logic here
            // other.GetComponent<PlayerHealth>()?.TakeDamage(999);
        }
    }
}