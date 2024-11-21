using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class RightController : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean buttonPressAction;
    private bool isTouched = false;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
