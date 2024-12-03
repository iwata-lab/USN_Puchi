using UnityEngine;
using Valve.VR.InteractionSystem;

public class CoinColorChange : MonoBehaviour
{
    private Renderer coinRenderer;  // コインのRenderer
    public static int coincount = 0; // コインの枚数
    private Color originalColor;  
    private Color previousColor;
    public bool isBlue = false;

    private bool hasChangedToBlue = false;  
    private ScoreManager scoreManager;


    void Start()
    {
        coinRenderer = GetComponent<Renderer>();
        scoreManager = FindObjectOfType<ScoreManager>();
        originalColor = coinRenderer.material.color;
        previousColor = originalColor;  // 元々の色を保存
    }

    // コインの色を変更するメソッド
    public void SetColor(Color color)
    {
        //Debug.Log($"Setting color to: {color}");
        if (color == Color.blue && !hasChangedToBlue)
        {
            coincount++;  // 青色に変更された場合、コインカウントを増加
            hasChangedToBlue = true;  // 青色に変更されたことを記録
            
            scoreManager.AddScore(5);

        }

        coinRenderer.material.color = color;  // コインの色を変更

        isBlue = (color == Color.blue);
    }

    public static int GetCoinCount()
    {
        return coincount;
    }

    // コインに触れたときのインタラクション処理（赤色に変化）
    public void OnHandHoverBegin(Hand hand)
    {
        if(!isBlue) // 青以外のときに赤にしたい
        {
            SetColor(Color.red);  // 触れた時に赤色に変更
        }

    }
    /*
    // コインから手が離れた時のインタラクション処理（元に戻す）
    public void OnHandHoverEnd(Hand hand)
    {
        if(!isBlue)
        {
            SetColor(originalColor);  // 離れた時に元の色に戻す
        }
    }
    */
}
