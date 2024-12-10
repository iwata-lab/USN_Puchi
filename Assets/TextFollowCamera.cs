using UnityEngine;

public class TextFollowCamera : MonoBehaviour
{
    public Camera vrCamera; // VRカメラ
    public Vector3 offset = new Vector3(0, 0, 2); // カメラからの距離（前方に2単位）

    void Update()
    {
        // カメラの位置を基にテキストの位置を更新
        if (vrCamera != null)
        {
            transform.position = vrCamera.transform.position + vrCamera.transform.forward * offset.z + vrCamera.transform.up * offset.y;
            // ヘッドセットの回転に合わせてテキストの向きを調整
            transform.rotation = vrCamera.transform.rotation;
        }
    }
}
