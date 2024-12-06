using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 8f;    // 移動速度
    public float respawnDelay = 3f; // 再び現れるまでの時間
    public float startDelay = 0f;   // スタートするまでの時間

    public Vector3 startPoint = new Vector3(55f, 0f, 12.5f);  // スタート地点の座標
    public Vector3 endPoint = new Vector3(-78f, 0f, 12.5f);    // エンド地点の座標
    public Vector3 target;  // 現在の目標地点

    public SignalManager signalManager; // 信号の状態を管理するオブジェクトを参照

    private bool isActive = false;  // 車が動き始めるかどうか
    private bool isMoving = false;  // 車が動いているかどうか

    private bool isXAxisCar;    // 車がx軸方向に動くかどうか

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPoint;
        target = endPoint;

        // 車がx軸方向に動くかどうかを判定
        float rotationY = transform.rotation.eulerAngles.y;
        isXAxisCar = !(Mathf.Approximately(rotationY, 0f) || Mathf.Approximately(rotationY, 180f));

        StartCoroutine(StartAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive) return;

        // 信号の状態に応じて車を動かすかどうかを決定
        bool canMove = CheckSignalState();
        if (canMove)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        // エンド地点に到達した場合
        if (Vector3.Distance(transform.position, endPoint) < 0.1f && target == endPoint)
        {
            StartCoroutine(Respawn());
        }
    }


    private IEnumerator Respawn()
    {
        gameObject.GetComponent<Renderer>().enabled = false;    // 車を非表示にする
        yield return new WaitForSeconds(respawnDelay);

        // スタート地点に戻して再表示する
        transform.position = startPoint;
        gameObject.GetComponent<Renderer>().enabled = true;

    }

    private IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        isActive = true;    // 車をスタート
    }

    private bool CheckSignalState()
    {
        // x軸方向の車なら、信号がGreenのときに動く
        if (isXAxisCar)
        {
            return signalManager.GetXSignalState() == SignalManager.SignalState.Green;
        }
        // z軸方向の車なら、信号がRedのときに動く
        else
        {
            return signalManager.GetXSignalState() == SignalManager.SignalState.Red;
        }
    }
}
