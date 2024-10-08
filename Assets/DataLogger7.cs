using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataLogger7: MonoBehaviour
{
    private StreamWriter sw;
    private FileInfo fi;

    private DateTime date;

    private float a ;
    private float b ;
    private float c ;
    private float d ;
    // Use this for initialization
    void Start()
    {
        date = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        a = Input.GetAxis("Horizontal");
        b = Input.GetAxis("Vertical");
        c = Input.GetAxis("Fire1");
        d = Input.GetAxis("Fire2");

        string str;
        string format = "yyyy-MM-dd-HH-mm-ss";
        //string filename = "/data/" + date.ToString(format) + "joycon" + ".csv";
        string filename = "/data/" + "joycon" + ".csv";
        fi = new FileInfo(Application.dataPath + filename);
        str = Time.time + "," + a + "," + b + "," + c + "," + d;
        sw = fi.AppendText();
        sw.WriteLine(str);
        sw.Flush();
        sw.Close();

        //Debug.Log(a);
    }
    
}