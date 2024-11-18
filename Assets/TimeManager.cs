using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TimeManager;
using UnityEngine.SceneManager;

public class TimeManager : MonoBehaviour
{
    public float timeLimit = 180;   // 制限時間180秒
    private float timer;

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
        int finalScore = FindObjectOfType<ScoreManager>().GetScore;
        PlayerPrefs.SetInt("FinalScore",finalScore);

        // クリア画面に遷移
        SceneManager.LoadScene("ScoreScene");
    }

}
