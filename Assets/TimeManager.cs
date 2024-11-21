using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static float timeLimit = 180.0f;   // 制限時間180秒
    public float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
      timer = timeLimit;  
    }

    // Update is called once per frame
    void Update()
    {
      timer -= Time.deltaTime;

      if (timer <= 0) 
      {
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
