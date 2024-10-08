using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMD : MonoBehaviour
{
    public static float position_x;
    public static float position_y;
    public static float position_z;
    public static float rotation_x;
    public static float rotation_y;
    public static float rotation_z;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position_x = this.gameObject.transform.position.x;
        position_y = this.gameObject.transform.position.y;
        position_z = this.gameObject.transform.position.z;    
        //rotation_x = this.gameObject.transform.eulerAngles.x;
        //rotation_y = this.gameObject.transform.eulerAngles.y;
        //rotation_z = this.gameObject.transform.eulerAngles.z;

        var quaternion = this.gameObject.transform.rotation;
        var kaku = Quaternion.Inverse(quaternion);
        //Debug.Log(kaku);
        rotation_x = kaku.eulerAngles.x;
        rotation_y = kaku.eulerAngles.y;
        rotation_z = kaku.eulerAngles.z;

        /*rotation_y = this.gameObject.transform.rotation.y;
        rotation_y = Quaternion.Inverse(rotation_y);
        rotation_y = rotation_y.eulerAngles;*/

        /*if(rotation_x>180){
            rotation_x = rotation_x - 360;
        }
        if(rotation_y>180){
            rotation_y = rotation_y - 360;
        }
        if(rotation_z>180){
            rotation_z = rotation_z - 360;
        }*/
    }
}
