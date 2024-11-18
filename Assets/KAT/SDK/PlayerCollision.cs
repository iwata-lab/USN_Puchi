using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // 衝突検知メソッド
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトが「Obstacle」タグを持つ場合
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            scoreManager.SubtractScore(10);
        }

    }

}
