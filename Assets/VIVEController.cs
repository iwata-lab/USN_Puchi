using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VIVEController : MonoBehaviour
{
    public SteamVR_Action_Boolean triggerAction;  // トリガーボタンのアクション（例: Trigger）
    public SteamVR_Input_Sources handType;        // 使用する手（例: RightHand）

    private GameObject currentCoin;                // 現在インタラクション中のコイン

    void Start()
    {
        // 初期設定（必要に応じて）
    }

    void Update()
    {
        // トリガーボタンが押された場合
        if (triggerAction.GetStateDown(handType) && currentCoin != null)
        {
            // トリガーが押されたときはコインを青色に変更
            currentCoin.GetComponent<CoinColorChange>().SetColor(Color.blue);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // コインに触れた場合
        if (other.CompareTag("Coin"))
        {
            currentCoin = other.gameObject;
            // コインに触れた時に赤色に変更
            Debug.Log("CoinTouch");
            currentCoin.GetComponent<CoinColorChange>().SetColor(Color.red);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // コインから手が離れた場合
        if (other.CompareTag("Coin"))
        {
            currentCoin = null;
        }
    }
}
