using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score = 100;    // 初期スコア：100点

    public void AddScore(int amount)
    {
        score += amount;
        //Debug.Log("スコアが増えました：" + score);
    }

    public void SubtractScore(int amount)
    {
        score -= amount;
        if (score < 0) score = 0;   // scoreがマイナスにならないように
        //Debug.Log("スコアが減りました：" + score);
    }

    public static int GetScore()
    {
        return score;
    }

}
