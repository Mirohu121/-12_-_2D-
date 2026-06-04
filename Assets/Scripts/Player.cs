using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 12f;
    public float diveForce = 25f;

    private Rigidbody2D rb;
    private int jumpCount = 0;
    private const int maxJumps = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
        }

        if (Input.GetMouseButton(0) && rb.linearVelocity.y < 0)
        {
            rb.AddForce(Vector2.down * diveForce, ForceMode2D.Force);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        jumpCount = 0;
    }
}