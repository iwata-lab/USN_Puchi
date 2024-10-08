using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class Key1 : MonoBehaviour
            {
    int count = 0;
     private float a ;
    private float b ;
    private float c ;
    private float d ;
                //csv
                public string filename = "count";
                StreamWriter sw;

    private DateTime date;

                // Use this for initialization
                private void Start()
                {
                    sw = new StreamWriter(@"" + filename + ".csv", false);
                    string[] s1 = { "count", "time" };
                    string s2 = string.Join(",", s1);
                    sw.WriteLine(s2);
                }

                // Update is called once per frame
   private void Update()
                {
       
       a = Input.GetAxis("Horizontal");
        b = Input.GetAxis("Vertical");
        c = Input.GetAxis("Fire1");
        d = Input.GetAxis("Fire2");
         string[] str = { "" + a, "" + UnityEngine.Time.time };
            string str2 = string.Join(",", str);
            sw.WriteLine(str2);
      /*   if (Input.GetKeyDown(KeyCode.Joystick2Button2))
        {
            count += 1;       
            string[] str = { "" + count, "" + UnityEngine.Time.time };
            string str2 = string.Join(",", str);
            sw.WriteLine(str2);
        }
*/
        if (Input.GetKeyDown(KeyCode.H))
        {
            sw.Close();

        }


    }
  }
        

/*
public class Key : MonoBehaviour
{
    private StreamWriter sw;
    private FileInfo fi;

    private DateTime date;

    int count = 0;
    // Use this for initialization
    void Start()
    {
        date = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            count += 1;
            Debug.Log(count);
        }
        string str;
        string format = "yyyy-MM-dd-HH-mm-ss";
        string filename = "/data/" + date.ToString(format) + ".csv";
        fi = new FileInfo(Application.dataPath + filename);
        str = count + "," + Time.time;
        sw = fi.AppendText();
        sw.WriteLine(str);
        sw.Flush();
        sw.Close();
    }
}
*/