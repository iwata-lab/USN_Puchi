using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalLight : MonoBehaviour
{
    public Light red;
    public Light yellow;
    public Light green;
    

    private float lightTimer;    // 内部タイマー
    private int currentLight = 0;   // 現在のライト（0:緑，1:黄，2:赤）

    // 各ライトの点灯時間
    private float greenDuration = 12f;
    private float yellowDuration = 3f;
    private float redDuration = 12f;

    public void SetInitialState(SignalManager.SignalState initialState)
    {
        lightTimer = 0f;
        red.enabled = false;
        yellow.enabled = false;
        green.enabled = false;

        switch (initialState)
        {
            case SignalManager.SignalState.Green:
                green.enabled = true;
                currentLight = 0;
                break;
            case SignalManager.SignalState.Yellow:
                yellow.enabled = true;
                currentLight = 1;
                break;
            case SignalManager.SignalState.Red:
                red.enabled = true;
                currentLight = 2;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lightTimer = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        lightTimer += Time.deltaTime;

        float currentDuration = GetCurrentLightDuration();

        // 点灯時間を超えたら次のライトへ切替
        if (lightTimer >= currentDuration)
        {
            lightTimer = 0f;
            ChangeLight();
        }
    }

    // 現在のライトの点灯時間を取得
    private float GetCurrentLightDuration()
    {
        switch(currentLight)
        {
            case 0: return greenDuration;
            case 1: return yellowDuration;
            case 2: return redDuration;
            default: return 0f;
        }
    }

    private void ChangeLight()
    {
        // 全てのライトを一旦オフ
        red.enabled = false;
        yellow.enabled = false;
        green.enabled = false;

        switch(currentLight)
        {
            case 0: // 緑 → 黄
                yellow.enabled = true;
                currentLight = 1;
                //Debug.Log("Yellow Light ON");
                break;
            case 1: // 黄 → 赤
                red.enabled = true;
                currentLight = 2;
                //Debug.Log("Red Light ON");
                break;
            case 2: // 赤 → 緑
                green.enabled = true;
                currentLight = 0;
                //Debug.Log("Green Light ON");
                break;
        }

    }
}
