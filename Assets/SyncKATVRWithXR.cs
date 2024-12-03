using UnityEngine;

public class SyncKATVRWithXR : MonoBehaviour
{
    public GameObject katvrWalker; // KATVR の歩行システム
    public GameObject xrRigCamera; // XR Rig のカメラ
    public GameObject xrRig;       // XR Rig 自体

    private Vector3 previousPosition;

    void Start()
    {
        // 初期位置を保存
        previousPosition = katvrWalker.transform.position;
    }

    void Update()
    {
        // KATVR Walker の移動に基づいて XR Rig を同期
        if (katvrWalker != null && xrRig != null)
        {
            // KATVR Walker の移動量を計算して、XR Rig を同期
            Vector3 movement = katvrWalker.transform.position - previousPosition;
            xrRig.transform.position += movement;

            // KATVR Walker のカメラ位置を XR Rig のカメラに同期
            if (xrRigCamera != null)
            {
                xrRigCamera.transform.position = katvrWalker.transform.position;
                xrRigCamera.transform.rotation = katvrWalker.transform.rotation;
            }

            // 前回の位置を更新
            previousPosition = katvrWalker.transform.position;
        }
    }
}
