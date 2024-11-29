using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    private ScoreManager scoreManager;
    public static int collisionCount = 0;

    private CollisionText collisiontText;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        collisionText = FindObjectOfType<CollisionText>();

    }

    // 衝突検知メソッド
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトが「Obstacle」タグを持つ場合
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            scoreManager.SubtractScore(10);

            collisionCount ++;  // 衝突した回数を数える
            Debug.Log("衝突回数：" + collisionCount);

            // 衝突テキストを表示
            if (collisionText != null)
            {
                collisionText.DisplayCollisionText();
            }
        }

    }

    public static int GetCollision()
    {
        return collisionCount;
    }


}
