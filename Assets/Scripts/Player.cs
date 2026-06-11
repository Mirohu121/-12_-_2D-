using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Cysharp.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    [Header("ジャンプ力")]
    public float jumpForce = 12f;
    [Header("急降下させる力")]
    public float diveForce = 25f;
    [Header("ジャンプ時に前に進む力")]
    public float forwardForce = 3f;
    [Header("画面のUIテキスト")]
    public TextMeshProUGUI scoreText;

    private Rigidbody2D rb;
    private int jumpCount = 0;
    private const int maxJumps = 2;
    private float score = 0f;
    private bool isGameOver = false;

    private float leftDeathLimit = -9f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isGameOver) return;

        // 1. ジャンプ機能（上に跳ぶと同時に、少し右に慣力をつける）
        if (Input.GetMouseButtonDown(0) && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(forwardForce, jumpForce);
            jumpCount++;
        }

        // 2. 急降下機能
        if (Input.GetMouseButton(0) && rb.linearVelocity.y < 0)
        {
            rb.AddForce(Vector2.down * diveForce, ForceMode2D.Force);
        }

        // 3. 画面の左端（カメラ外）に置いていかれたら死ぬチェック
        if (transform.position.x <= leftDeathLimit)
        {
            GameOverRoutine().Forget();
        }

        // 4. スコアアップ機能
        score += Time.deltaTime * 10f;
        scoreText.text = "SCORE: " + Mathf.FloorToInt(score).ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpCount = 0; // 床に着地したらジャンプ回数リセット
    }

    // トゲに当たって死ぬ処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Obstacle" && !isGameOver)
        {
            GameOverRoutine().Forget();
        }
    }

    // UniTaskを使ったゲームオーバー＆リトライ待ち処理
    private async UniTaskVoid GameOverRoutine()
    {
        isGameOver = true;
        Time.timeScale = 0f; //時間を止める

        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.R));

        Time.timeScale = 1f; // 時間を戻す
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // シーン再読み込み
    }
}