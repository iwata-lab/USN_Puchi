using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionText : MonoBehaviour
{

    public gameObject collisionText;
    private Coroutine displayCoroutine;

    void Start()
    {
        if (collisionText != null)
        {
            collisionText.SetActive(false); // 初期状態では非表示
        }        
    }

    public void DisplayCollisionText()
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine); // 前の表示処理を停止
        }
        displayCoroutine = StartCoroutine(DisplayTextCoroutine());
    }

    // テキストを一定時間表示し、非表示にするコルーチン
    private IEnumerator DisplayTextCoroutine()
    {
        if (collisionText != null)
        {
            collisionText.SetActive(true); // テキストを表示
            yield return new WaitForSeconds(3f); // 3秒待機
            collisionText.SetActive(false); // テキストを非表示
        }
    }    


    // Update is called once per frame
    void Update()
    {
        
    }
}
