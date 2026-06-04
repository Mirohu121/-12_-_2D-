using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f; 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


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
    //GameOver
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Nature_props_18")
        {
            Debug.Log("ゲームオーバー！");

            Time.timeScale = 0f;
        }
    }
}