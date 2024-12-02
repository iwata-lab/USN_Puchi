using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    private Vector3 startPosition; // 初期位置
    public static float distance = 0f; // 移動距離

    void Start()
    {
        // プレイヤーの初期位置を記録
        startPosition = transform.position;
    }

    void Update()
    {
        // 現在位置との差分を計算して移動距離を更新
        distance = Vector3.Distance(startPosition, transform.position);
    }

    public static float GetTotalDistance()
    {
        return distance;
    }
}
