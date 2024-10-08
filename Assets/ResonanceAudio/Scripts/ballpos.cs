using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballpos : MonoBehaviour
{
    public static Vector3 position;
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
        position = this.gameObject.transform.position;
        position_x = this.gameObject.transform.position.x;
        position_y = this.gameObject.transform.position.y;
        position_z = this.gameObject.transform.position.z;    
        rotation_x = this.gameObject.transform.eulerAngles.x * Mathf.PI / 180;
        rotation_y = this.gameObject.transform.eulerAngles.y * Mathf.PI / 180;
        rotation_z = this.gameObject.transform.eulerAngles.z * Mathf.PI / 180;
    }
}
