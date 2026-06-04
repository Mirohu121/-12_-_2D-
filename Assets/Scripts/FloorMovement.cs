using UnityEngine;

public class FloorMovement : MonoBehaviour
{
    [Header("床の動くスピード")]
    public float moveSpeed = 5f;

    [Header("どこまで左に行ったらワープするか（インスペクターで調整）")]
    public float leftLimit = -11f;

    [Header("右にどれだけワープするか（床3枚分の合計の幅）")]
    public float rightWarpDistance = 24.6f; // 動画の初期配置から計算した数値です

    void Update()
    {
        // 毎フレーム、左方向へ移動
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // 境界線を超えたら、1回だけ右にテレポート
        if (transform.position.x <= leftLimit)
        {
            Vector3 newPos = transform.position;
            newPos.x += rightWarpDistance;
            transform.position = newPos;
        }
    }
}