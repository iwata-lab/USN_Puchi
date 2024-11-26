using UnityEngine;
using Valve.VR.InteractionSystem;

public class CoinColorChange : MonoBehaviour
{
    private Renderer coinRenderer;  // コインのRenderer
    public static int coincount = 0; // コインの枚数
    private Color originalColor;     

    void Start()
    {
        coinRenderer = GetComponent<Renderer>();
        originalColor = coinRenderer.material.color;
    }

    // コインの色を変更するメソッド
    public void SetColor(Color color)
    {
        if (color == Color.blue)
        {
            coincount++;  // 青色に変更された場合、コインカウントを増加
        }

        coinRenderer.material.color = color;  // コインの色を変更
    }

    public static int GetCoinCount()
    {
        return coincount;
    }

    // コインに触れたときのインタラクション処理（赤色に変化）
    public void OnHandHoverBegin(Hand hand)
    {
        SetColor(Color.red);  // 触れた時に赤色に変更
    }

    // コインから手が離れた時のインタラクション処理（元に戻す）
    public void OnHandHoverEnd(Hand hand)
    {
        SetColor(originalColor);  // 離れた時に元の色に戻す
    }
}
