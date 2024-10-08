using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using Random = UnityEngine.Random;

public class Guide : MonoBehaviour
{
    public GameObject Male;
    public static GameObject front8;
    public static GameObject Rfront8;
    public static GameObject R8;
    public static GameObject Rback8;
    public static GameObject back8;
    public static GameObject Lback8;
    public static GameObject L8;
    public static GameObject Lfront8;
    public static GameObject front2;
    public static GameObject Rfront2;
    public static GameObject R2;
    public static GameObject Rback2;
    public static GameObject back2;
    public static GameObject Lback2;
    public static GameObject L2;
    public static GameObject Lfront2;
    public List<GameObject> List1 = new List<GameObject>();
    public int t=0;
    public List<int> guidesuuji = new List<int>(){0,0,0,1,1,1,2,2,2,3,3,3,4,4,4,5,5,5,6,6,6,7,7,7,8,8,8,9,9,9,10,10,10,11,11,11,12,12,12,13,13,13,14,14,14,15,15,15};
    //public int suujicount = suuji.Count;
    TextAsset guidefile; // CSVファイル
    List<string[]> guidecsv = new List<string[]>(); // CSVの中身を入れるリスト;
    public Vector3 position;
    public Quaternion rotation;
    public int guideban;
    public float xguide;
    public float yguide;
    public float zguide;
    private StreamWriter sw;
    public float timeStart;
    public string anspath;
    public string ansfilepath;
    public string neckpath;
    public string neckfilepath;
    public float timeNow;
    private int judge;
    private float guiderad;
    private float guidedeg;
    private float guidedis;
    public Vector3 guidesaypos;
    public Vector3 guidepos;
    private float guidesayx;
    private float guidesayy;
    private float guidesayz;
    private KeyCode[] _key = new KeyCode[] 
    {
        KeyCode.Alpha1, KeyCode.Alpha2, 
        KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, 
        KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, 
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I
    };
    public List<GameObject> guidelist = new List<GameObject>()
    {
        front8, Rfront8, R8, Rback8, back8, Lback8, L8, Lfront8,
        front2, Rfront2, R2, Rback2, back2, Lback2, L2, Lfront2
    };
    //[SerializeField] private Transform Male;

    // Start is called before the first frame update
    void Start()
    {
        timeStart = Time.realtimeSinceStartup;

        guidefile = Resources.Load("zahyouguide") as TextAsset; // Resouces下のCSV読み込み
        StringReader guidereader = new StringReader(guidefile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (guidereader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string guideline = guidereader.ReadLine(); // 一行ずつ読み込み
            guidecsv.Add(guideline.Split(',')); // , 区切りでリストに追加
        }

        //書き込み用csv
        ansfilepath = anspath + @"Datas\1_guideAns_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter streamWriter = new StreamWriter(ansfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] streamWriter1 = {"Ans","Say","judge","timenow","guidedis","guide_position_x","guide_position_y","guide_position_z","guide_say_position_x","guide_say_position_y","guide_say_position_z"};
        string streamWriter2 = string.Join(",", streamWriter1);
        streamWriter.WriteLine(streamWriter2);
        streamWriter.Close();

        neckfilepath = neckpath + @"Datas\1_guideneck_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter neckstreamWriter = new StreamWriter(neckfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] neckstreamWriter1 = {"Ans","timenow","HMD_rotation_x","HMD_rotation_y","HMD_rotation_z","guidedeg","guide_position_x", "guide_position_y","guide_position_z"};
        string neckstreamWriter2 = string.Join(",", neckstreamWriter1);
        neckstreamWriter.WriteLine(neckstreamWriter2);
        neckstreamWriter.Close();
        
        //isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        //TitleScore.GetComponent<TextMesh>().text = "試験を選択してください\na →　Hey\nb　→　Ball\nc　→　Guide\nd　→　Hey&Ball\ne　→　Hey&Guide\nf　→　Ball&Guide\ng　→　All";
        if (Input.GetKeyDown (KeyCode.RightArrow)){
            if (guidesuuji.Count == 0)
            {
                UnityEditor.EditorApplication.isPlaying = false;

            }else{
                var guidenum = Random.Range(0, guidesuuji.Count);
                guideban = guidesuuji[guidenum];
                Debug.Log(guideban);
                Debug.Log(guidecsv[guideban][0]);
                Debug.Log(guidesuuji.Count);

                xguide = float.Parse(guidecsv[guideban][0]);
                yguide = float.Parse(guidecsv[guideban][1]);
                zguide = float.Parse(guidecsv[guideban][2]);

                GameObject recog = Instantiate(guidelist[guideban], new Vector3(0f, 0f, 8f), Quaternion.identity)　as GameObject;

                //GameObject recog = Instantiate(Male, new Vector3(0, 0, 0), Quaternion.identity)　as GameObject;
                List1.Add(recog);
                Destroy(recog, 2.5f);

                guidesuuji.RemoveAt(guidenum);
            }
        }

        timeNow =  Time.realtimeSinceStartup - timeStart;
        
        outputcsv();

        guiderad = Mathf.Atan2(zguide, xguide);
        guidedeg = guiderad * Mathf.Rad2Deg -90;
        if(guidedeg<0){
            guidedeg = guidedeg + 360;
        }

        StreamWriter neckstreamWriter = new StreamWriter(neckfilepath, true, Encoding.GetEncoding("utf-8"));
        string[] neckstreamWriter1 = {guideban.ToString(),timeNow.ToString(),HMD.rotation_x.ToString(),HMD.rotation_y.ToString(),HMD.rotation_z.ToString(),guidedeg.ToString(), xguide.ToString(), yguide.ToString(), zguide.ToString()};
        string neckstreamWriter2 = string.Join(",", neckstreamWriter1);
        neckstreamWriter.WriteLine(neckstreamWriter2);
        neckstreamWriter.Close();

        //Debug.Log(HMD.rotation_y);
    }


    void outputcsv()
    {

        if (Input.anyKeyDown)
        {
            for(int i = 0; i < _key.Length; i++)
            {
                if (Input.GetKeyDown(_key[i])){
                    
                    
                    if(guideban == i){
                        judge = 1;
                    }else{
                        judge = 0;
                    }

                    guidesayx = float.Parse(guidecsv[i][0]);
                    guidesayy = float.Parse(guidecsv[i][1]);
                    guidesayz = float.Parse(guidecsv[i][2]);
                    guidesaypos = new Vector3 (guidesayx,guidesayy,guidesayz);
                    guidepos = new Vector3 (xguide, yguide, zguide);
                    guidedis = Vector3.Distance(guidepos, guidesaypos);

                    StreamWriter streamWriter = new StreamWriter(ansfilepath, true, Encoding.GetEncoding("utf-8"));
                    string[] streamWriter1 = {guideban.ToString() ,i.ToString(),judge.ToString(),timeNow.ToString(),guidedis.ToString(), xguide.ToString(), yguide.ToString(), zguide.ToString(),guidecsv[i][0].ToString(),guidecsv[i][1].ToString(),guidecsv[i][2].ToString()};
                    string streamWriter2 = string.Join(",", streamWriter1);
                    streamWriter.WriteLine(streamWriter2);
                    streamWriter.Close();
                }
            }    
        }
    }
}
