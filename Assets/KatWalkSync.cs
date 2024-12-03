using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatWalkSync : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform katWalker;
    public Transform CameraRig;
    public float katWalkScale = 3.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraRig.position = new Vector3(katWalker.position.x * katWalkScale , CameraRig.position.y,katWalker.position.z * katWalkScale);
        CameraRig.rotation = Quaternion.Euler(0,katWalker.eulerAngles.y,0);
    }
}
