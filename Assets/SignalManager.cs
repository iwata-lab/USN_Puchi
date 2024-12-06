using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalManager : MonoBehaviour
{
    public enum SignalState { Green, Yellow, Red } // 信号の状態
    public SignalLight[] signalLights; // 信号機の配列

    public List<SignalLight> xSignals = new List<SignalLight>();   // x軸方向の信号機
    public List<SignalLight> zSignals = new List<SignalLight>();   // z軸方向の信号機

    void Start()
    {
        foreach (SignalLight signalLight in signalLights)
        {
            // 各信号機の初期状態を設定
            float rotationY = signalLight.transform.rotation.eulerAngles.y;

            if (Mathf.Approximately(rotationY, -90f) || Mathf.Approximately(rotationY, 90f))
            {
                xSignals.Add(signalLight);
                signalLight.SetInitialState(SignalState.Green);
            }
            else if (Mathf.Approximately(rotationY, 0f) || Mathf.Approximately(rotationY, 180f))
            {
                zSignals.Add(signalLight);
                signalLight.SetInitialState(SignalState.Red);
            }
        }
    }

    void Update()
    {
        // x軸方向の信号の状態を取得
        SignalState xState = GetXSignalState();

        // z軸方向の信号に動作を同期
        SetZSignalState(xState);
    }

    public SignalState GetXSignalState()
    {
        if (xSignals.Count > 0)
        {
            if (xSignals[0].green.enabled) return SignalState.Green;
            if (xSignals[0].yellow.enabled) return SignalState.Yellow;
            if (xSignals[0].red.enabled) return SignalState.Red;
        }
        return SignalState.Red;
    }


    public void SetZSignalState(SignalState xState)
    {
        SignalState zState;
        switch (xState)
        {
            case SignalState.Green:
                zState = SignalState.Red;
                break;
            case SignalState.Yellow:
                zState = SignalState.Yellow;
                break;
            case SignalState.Red:
                zState = SignalState.Green;
                break;
            default:
                zState = SignalState.Red;
                break;
        }

        // z軸方向の信号を切り替え
        foreach (SignalLight signalLight in zSignals)
        {
            signalLight.SetInitialState(zState);
        }
    }


}
