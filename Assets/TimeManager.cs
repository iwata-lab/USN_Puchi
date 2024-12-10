using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class TimeManager : MonoBehaviour
{
    public static float timeLimit = 90.0f;   // 制限時間120秒
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
      int finalScore = ScoreManager.GetScore(); 
      PlayerPrefs.SetInt("FinalScore", finalScore);

      // クリア画面に遷移
      SceneManager.LoadScene("ScoreScene");

      // 5秒後にPlayモードを終了
      Invoke("QuitGame", 5.0f);
  }

  void QuitGame()
  {
      Debug.Log("Attempring to quit play mode");

      #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false; // Unityエディタ用
      #else
      Application.Quit(); // ビルド後の実行環境用
      #endif
  }


}
