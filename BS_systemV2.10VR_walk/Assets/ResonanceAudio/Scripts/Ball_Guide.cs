using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using Random = UnityEngine.Random;

public class Ball_Guide : MonoBehaviour
{
    public GameObject sball;
    public List<GameObject> List1 = new List<GameObject>();
    private List<int> ballsuuji = new List<int>(){0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9,10,10,11,11,12,12,13,13,14,14,15,15,16,16,17,17,18,18,19,19,20,20,21,21,22,22,23,23};
    TextAsset ballFile; // CSVファイル
    TextAsset tarFile; // CSVファイル
    List<string[]> ballcsv = new List<string[]>(); // CSVの中身を入れるリスト;
    List<string[]> tarcsv = new List<string[]>(); // CSVの中身を入れるリスト;
    public Vector3 position;
    public Quaternion rotation;
    public int ballban;
    public float xball;
    public float yball;
    public float zball;
    private StreamWriter sw;
    public float timeStart;
    private string ballanspath;
    private string ballansfilepath;
    private string ballneckpath;
    private string ballneckfilepath;
    public float timeNow;
    private int balljudge;
    private float ballrad;
    private float balldeg;
    private float balldis;
    public Vector3 ballsaypos;
    private float ballsayx;
    private float ballsayy;
    private float ballsayz;
    private KeyCode[] _key = new KeyCode[] 
    {
        KeyCode.Alpha1, KeyCode.Alpha2, 
        KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, 
        KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, 
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I,
        KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K
    };
    private KeyCode[] ballkey = new KeyCode[] 
    {
        KeyCode.Alpha1, KeyCode.Alpha2, 
        KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, 
        KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, 
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O,
        KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H
    };
    public static Vector3 target;
    public static Vector3 beforeball;
    
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
    
    private List<int> guidesuuji = new List<int>(){0,0,0,1,1,1,2,2,2,3,3,3,4,4,4,5,5,5,6,6,6,7,7,7,8,8,8,9,9,9,10,10,10,11,11,11,12,12,12,13,13,13,14,14,14,15,15,15};
    //public int suujicount = suuji.Count;
    TextAsset guidefile; // CSVファイル
    List<string[]> guidecsv = new List<string[]>(); // CSVの中身を入れるリスト;
    
    public int guideban;
    public float xguide;
    public float yguide;
    public float zguide;
    public string guideanspath;
    public string guideansfilepath;
    public string guideneckpath;
    public string guideneckfilepath;
    private int guidejudge;
    private float guiderad;
    private float guidedeg;
    private float guidedis;
    public Vector3 guidesaypos;
    public Vector3 guidepos;
    private float guidesayx;
    private float guidesayy;
    private float guidesayz;
    
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
        
        //Ballの読み込み
        ballFile = Resources.Load("zahyouball") as TextAsset; // Resouces下のCSV読み込み
        StringReader ballreader = new StringReader(ballFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (ballreader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string ballline = ballreader.ReadLine(); // 一行ずつ読み込み
            ballcsv.Add(ballline.Split(',')); // , 区切りでリストに追加
        }

        tarFile = Resources.Load("zahyoutarget") as TextAsset; // Resouces下のCSV読み込み
        StringReader tarreader = new StringReader(tarFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (tarreader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = tarreader.ReadLine(); // 一行ずつ読み込み
            tarcsv.Add(line.Split(',')); // , 区切りでリストに追加
        }
        

        //Guideの読み込み
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
        ballansfilepath = ballanspath + @"Datas\2_3_ball_Ans_ballguide_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter ballstreamWriter = new StreamWriter(ballansfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] ballstreamWriter1 = {"Ans","Say","balljudge","timenow","balldis","before_position_x","before_position_y","before_position_z","before_say_position_x","before_say_position_y","before_say_position_z","after_position_x","after_position_y","after_position_z","after_say_position_x","after_say_position_y","after_say_position_z"};
        string ballstreamWriter2 = string.Join(",", ballstreamWriter1);
        ballstreamWriter.WriteLine(ballstreamWriter2);
        ballstreamWriter.Close();

        ballneckfilepath = ballneckpath + @"Datas\2_3_ball_neck_ballguide_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter ballneckstreamWriter = new StreamWriter(ballneckfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] ballneckstreamWriter1 = {"Ans","timenow","HMD_rotation_x","HMD_rotation_y","HMD_rotation_z","balldeg","ball_position_x", "ball_position_y","ball_position_z"};
        string ballneckstreamWriter2 = string.Join(",", ballneckstreamWriter1);
        ballneckstreamWriter.WriteLine(ballneckstreamWriter2);
        ballneckstreamWriter.Close();

        guideansfilepath = guideanspath + @"Datas\2_3_guide_Ans_ballguide_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter guidestreamWriter = new StreamWriter(guideansfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] guidestreamWriter1 = {"Ans","Say","guidejudge","timenow","guidedis","guide_position_x","guide_position_y","guide_position_z","guide_say_position_x","guide_say_position_y","guide_say_position_z"};
        string guidestreamWriter2 = string.Join(",", guidestreamWriter1);
        guidestreamWriter.WriteLine(guidestreamWriter2);
        guidestreamWriter.Close();

        guideneckfilepath = guideneckpath + @"Datas\2_3_guide_neck_ballguide_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter guideneckstreamWriter = new StreamWriter(guideneckfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] guideneckstreamWriter1 = {"Ans","timenow","HMD_rotation_x","HMD_rotation_y","HMD_rotation_z","guidedeg","guide_position_x", "guide_position_y","guide_position_z"};
        string guideneckstreamWriter2 = string.Join(",", guideneckstreamWriter1);
        guideneckstreamWriter.WriteLine(guideneckstreamWriter2);
        guideneckstreamWriter.Close();
        
        //isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        //TitleScore.GetComponent<TextMesh>().text = "試験を選択してください\na →　Hey\nb　→　Ball\nc　→　Guide\nd　→　Hey&Ball\ne　→　Hey&Guide\nf　→　Ball&Guide\ng　→　All";
        if (Input.GetKeyDown (KeyCode.RightArrow)){
            //Ballの生成
            if (ballsuuji.Count == 0)
            {
                UnityEditor.EditorApplication.isPlaying = false;

            }else{
                var ballnum = Random.Range(0, ballsuuji.Count);
                ballban = ballsuuji[ballnum];
                Debug.Log(ballban);
                Debug.Log(ballcsv[ballban][0]);
                Debug.Log(ballsuuji.Count);

                xball = float.Parse(ballcsv[ballban][0]);
                yball = float.Parse(ballcsv[ballban][1]);
                zball = float.Parse(ballcsv[ballban][2]);
                beforeball = new Vector3(xball,yball,zball);

                target = new Vector3(float.Parse(tarcsv[ballban][0]),float.Parse(tarcsv[ballban][1]),float.Parse(tarcsv[ballban][2]));
                

                GameObject move = Instantiate(sball, new Vector3(xball,yball,zball), Quaternion.identity)　as GameObject;
                List1.Add(move);
                //move.transform.position = Vector3.MoveTowards(move.transform.position, target, speed * Time.deltaTime);

                //GameObject stop = Instantiate(Male, new Vector3(0, 0, 0), Quaternion.identity)　as GameObject;
                
                //Destroy(move, 2.0f);
                ballsuuji.RemoveAt(ballnum);
            }

            //Guideの生成
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
                Destroy(recog, 3.0f);

                guidesuuji.RemoveAt(guidenum);
            }
        }
        
        //ballpos.position = Vector3.MoveTowards(ballpos.position, target, speed * Time.deltaTime);
        
        

        //Debug.Log(ballpos.position);

        timeNow =  Time.realtimeSinceStartup - timeStart;
        
        outputcsv();

        ballrad = Mathf.Atan2(ballpos.position_z, ballpos.position_x);
        balldeg = ballrad * Mathf.Rad2Deg-90;
        if(balldeg<0){
            balldeg = balldeg + 360;
        }

        StreamWriter ballneckstreamWriter = new StreamWriter(ballneckfilepath, true, Encoding.GetEncoding("utf-8"));
        string[] ballneckstreamWriter1 = {ballban.ToString(),timeNow.ToString(),HMD.rotation_x.ToString(),HMD.rotation_y.ToString(),HMD.rotation_z.ToString(),balldeg.ToString(), ballpos.position_x.ToString(),ballpos.position_y.ToString(),ballpos.position_z.ToString()};
        string ballneckstreamWriter2 = string.Join(",", ballneckstreamWriter1);
        ballneckstreamWriter.WriteLine(ballneckstreamWriter2);
        ballneckstreamWriter.Close();

        guiderad = Mathf.Atan2(zguide, xguide);
        guidedeg = guiderad * Mathf.Rad2Deg -90;
        if(guidedeg<0){
            guidedeg = guidedeg + 360;
        }

        StreamWriter guideneckstreamWriter = new StreamWriter(guideneckfilepath, true, Encoding.GetEncoding("utf-8"));
        string[] guideneckstreamWriter1 = {guideban.ToString(),timeNow.ToString(),HMD.rotation_x.ToString(),HMD.rotation_y.ToString(),HMD.rotation_z.ToString(),guidedeg.ToString(), xguide.ToString(), yguide.ToString(), zguide.ToString()};
        string guideneckstreamWriter2 = string.Join(",", guideneckstreamWriter1);
        guideneckstreamWriter.WriteLine(guideneckstreamWriter2);
        guideneckstreamWriter.Close();
    }


    void outputcsv()
    {
        if (Input.GetKey(KeyCode.X))
        {
            for(int j = 0; j < ballkey.Length; j++)
            {
                if (Input.GetKeyDown(ballkey[j])){
                    
                    
                    if(ballban == j){
                        balljudge = 1;
                    }else{
                        balljudge = 0;
                    }

                    ballsayx = float.Parse(ballcsv[j][0]);
                    ballsayy = float.Parse(ballcsv[j][1]);
                    ballsayz = float.Parse(ballcsv[j][2]);
                    ballsaypos = new Vector3 (ballsayx,ballsayy,ballsayz);
                    balldis = Vector3.Distance(beforeball, ballsaypos);

                    StreamWriter ballstreamWriter = new StreamWriter(ballansfilepath, true, Encoding.GetEncoding("utf-8"));
                    string[] ballstreamWriter1 = {ballban.ToString() ,j.ToString(),balljudge.ToString(),timeNow.ToString(),balldis.ToString(),xball.ToString(), yball.ToString(), zball.ToString(),ballcsv[j][0].ToString(),ballcsv[j][1].ToString(),ballcsv[j][2].ToString(), tarcsv[ballban][0], tarcsv[ballban][1], tarcsv[ballban][2], tarcsv[j][0],tarcsv[j][1],tarcsv[j][2]};
                    string ballstreamWriter2 = string.Join(",", ballstreamWriter1);
                    ballstreamWriter.WriteLine(ballstreamWriter2);
                    ballstreamWriter.Close();
                }
            }    
        }

        if (Input.GetKey(KeyCode.C))
        {
            for(int k = 0; k < _key.Length; k++)
            {
                if (Input.GetKeyDown(_key[k])){
                    
                    
                    if(guideban == k){
                        guidejudge = 1;
                    }else{
                        guidejudge = 0;
                    }

                    guidesayx = float.Parse(guidecsv[k][0]);
                    guidesayy = float.Parse(guidecsv[k][1]);
                    guidesayz = float.Parse(guidecsv[k][2]);
                    guidesaypos = new Vector3 (guidesayx,guidesayy,guidesayz);
                    guidepos = new Vector3 (xguide, yguide, zguide);
                    guidedis = Vector3.Distance(guidepos, guidesaypos);


                    StreamWriter guidestreamWriter = new StreamWriter(guideansfilepath, true, Encoding.GetEncoding("utf-8"));
                    string[] guidestreamWriter1 = {guideban.ToString() ,k.ToString(),guidejudge.ToString(),timeNow.ToString(),guidedis.ToString(), xguide.ToString(), yguide.ToString(), zguide.ToString(),guidecsv[k][0].ToString(),guidecsv[k][1].ToString(),guidecsv[k][2].ToString()};
                    string guidestreamWriter2 = string.Join(",", guidestreamWriter1);
                    guidestreamWriter.WriteLine(guidestreamWriter2);
                    guidestreamWriter.Close();
                }
            }    
        }
    }
}
