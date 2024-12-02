using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{
    // Start is called before the first frame update

    public Text ScoreText;
    int score;
    int totalScore;
    void Start()
    {
        totalScore = ScoreManager.GetScore();
        ScoreText.text = totalScore.ToString() + " 点";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
