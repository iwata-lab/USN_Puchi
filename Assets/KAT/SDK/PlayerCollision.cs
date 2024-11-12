using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public int score = 100;  // スコアの初期値

    // 衝突検知メソッド
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトが「Obstacle」タグを持つ場合
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // スコアが0以下にならないようにする
            if (score > 0)
            {
                score -= 10; // スコアを10減らす
                Debug.Log("障害物に衝突しました！スコア: " + score);
            }
        }
    }
}
