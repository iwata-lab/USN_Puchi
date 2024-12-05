using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalManager : MonoBehaviour
{
    public enum SignalState { Green, Yellow, Red } // 信号の状態
    public SignalLight[] signalLights; // 信号機の配列

    void Start()
    {
        foreach (SignalLight signalLight in signalLights)
        {
            // 各信号機の初期状態を設定
            float rotationY = signalLight.transform.rotation.eulerAngles.y;

            if (Mathf.Approximately(rotationY, -90f) || Mathf.Approximately(rotationY, 90f))
            {
                signalLight.SetInitialState(SignalState.Green);
            }
            else if (Mathf.Approximately(rotationY, 0f) || Mathf.Approximately(rotationY, 180f))
            {
                signalLight.SetInitialState(SignalState.Red);
            }
        }
    }
}
