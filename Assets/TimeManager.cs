using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TimeManager : MonoBehaviour
{
    public static float timeLimit = 40.0f;   // 制限時間180秒
    public float timer = 0.0f;

    public Text TimerText;
    private bool gameEnded = false;

    // Start is called before the first frame update
    void Start()
    {
      timer = timeLimit;  
    }

    // Update is called once per frame
    void Update()
    {
      if (gameEnded) return;  // 終了処理後は何もしない

      timer -= Time.deltaTime;
      TimerText.text = timer.ToString("F1") + " s";

      if (timer <= 0 && !gameEnded) 
      {
        gameEnded = true; // 終了処理実行フラグをセット
        EndGame();
      } 
    }

void EndGame()
{
    int finalScore = ScoreManager.GetScore(); // インスタンス化せずに直接呼び出す
    PlayerPrefs.SetInt("FinalScore", finalScore);

    // クリア画面に遷移
    SceneManager.LoadScene("ScoreScene");
}


}
