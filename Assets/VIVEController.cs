using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VIVEController : MonoBehaviour
{
    public SteamVR_Action_Boolean triggerAction; // ボタン（例: Trigger）を設定
    public SteamVR_Input_Sources handType; // 使用する手（例: RightHand）

    private GameObject currentCoin;
   
    void Start()
    {
        
    }

    void Update()
    {
        // トリガーボタンが押された場合
        if (triggerAction.GetStateDown(handType) && currentCoin != null)
        {
            currentCoin.GetComponent<CoinColorChange>().SetColor(Color blue); // 青色に変更
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        // Coinに触れた場合
        if (other.CompareTag("Coin"))
        {
            currentCoin = other.gameObject;
            currentCoin.GetComponent<Coin>().SetColor(Color.red); // 赤色に変更
        }

    void OnTriggerExit(Collider other)
    {
        // Coinから離れたらリセット
        if (other.CompareTag("Coin"))
        {
            currentCoin = null;
        }
    }

    }

}
