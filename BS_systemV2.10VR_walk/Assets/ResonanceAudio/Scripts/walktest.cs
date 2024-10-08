using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using Random = UnityEngine.Random;

public class walktest : MonoBehaviour
{
    public GameObject Male;
    public GameObject sball;
    public List<GameObject> List1 = new List<GameObject>();
    public int t=0;
    public List<int> suuji = new List<int>(){0,0,0,1,1,1,2,2,2,3,3,3,4,4,4,5,5,5,6,6,6,7,7,7,8,8,8,9,9,9,10,10,10,11,11,11,12,12,12,13,13,13,14,14,14,15,15,15};
    public List<int> ballsuuji = new List<int>(){0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9,10,10,11,11,12,12,13,13,14,14,15,15,16,16,17,17,18,18,19,19,20,20,21,21,22,22,23,23};
    //public int suujicount = suuji.Count;
    TextAsset csvFile;
    TextAsset ballFile; // CSVファイル
    TextAsset tarFile; // CSVファイル
    List<string[]> heycsv = new List<string[]>(); // CSVの中身を入れるリスト;
    List<string[]> ballcsv = new List<string[]>(); // CSVの中身を入れるリスト;
    List<string[]> tarcsv = new List<string[]>(); // CSVの中身を入れるリスト;
    public Vector3 position;
    public Quaternion rotation;
    public int ban;
    public float xhey;
    public float yhey;
    public float zhey;
    public int ballban;
    public float xball;
    public float yball;
    public float zball;
    private StreamWriter sw;
    public float timeStart;
    public string anspath;
    public string ansfilepath;
    public string neckpath;
    public string neckfilepath;
    private string ballanspath;
    private string ballansfilepath;
    private string ballneckpath;
    private string ballneckfilepath;
    public float timeNow;
    private int judge;
    private int balljudge;
    private float heyrad;
    private float heydegree;
    private float heydis;
    public Vector3 saypos;
    private float sayx;
    private float sayy;
    private float sayz;
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
    
    //[SerializeField] private Transform Male;

    // Start is called before the first frame update
    void Start()
    {
        timeStart = Time.realtimeSinceStartup;
        
        //heyの読み込み
        csvFile = Resources.Load("zahyouhey") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            heycsv.Add(line.Split(',')); // , 区切りでリストに追加
        }
        

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

        //書き込み用csv
        ansfilepath = anspath + @"Datas\2_1_hey_Ans_heyball_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter streamWriter = new StreamWriter(ansfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] streamWriter1 = {"Ans","Say","judge","timenow","heydis","hey_position_x","hey_position_y","hey_position_z","say_position_x","say_position_y","say_position_z"};
        string streamWriter2 = string.Join(",", streamWriter1);
        streamWriter.WriteLine(streamWriter2);
        streamWriter.Close();

        neckfilepath = neckpath + @"Datas\2_1_hey_neck_heyball_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter neckstreamWriter = new StreamWriter(neckfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] neckstreamWriter1 = {"Ans","timenow","HMD_rotation_x","HMD_rotation_y","HMD_rotation_z","heydegree","hey_position_x", "hey_position_y","hey_position_z"};
        string neckstreamWriter2 = string.Join(",", neckstreamWriter1);
        neckstreamWriter.WriteLine(neckstreamWriter2);
        neckstreamWriter.Close();

        ballansfilepath = ballanspath + @"Datas\2_1_ball_Ans_heyball_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter ballstreamWriter = new StreamWriter(ballansfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] ballstreamWriter1 = {"Ans","Say","balljudge","timenow","balldis","before_position_x","before_position_y","before_position_z","before_say_position_x","before_say_position_y","before_say_position_z","after_position_x","after_position_y","after_position_z","after_say_position_x","after_say_position_y","after_say_position_z"};
        string ballstreamWriter2 = string.Join(",", ballstreamWriter1);
        ballstreamWriter.WriteLine(ballstreamWriter2);
        ballstreamWriter.Close();

        ballneckfilepath = ballneckpath + @"Datas\2_1_ball_neck_heyball_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter ballneckstreamWriter = new StreamWriter(ballneckfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] ballneckstreamWriter1 = {"Ans","timenow","HMD_rotation_x","HMD_rotation_y","HMD_rotation_z","balldeg","ball_position_x", "ball_position_y","ball_position_z"};
        string ballneckstreamWriter2 = string.Join(",", ballneckstreamWriter1);
        ballneckstreamWriter.WriteLine(ballneckstreamWriter2);
        ballneckstreamWriter.Close();
        
        //isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        //TitleScore.GetComponent<TextMesh>().text = "試験を選択してください\na →　Hey\nb　→　Ball\nc　→　Guide\nd　→　Hey&Ball\ne　→　Hey&Guide\nf　→　Ball&Guide\ng　→　All";
        if (Input.GetKeyDown (KeyCode.RightArrow)){
            //heyの生成
            if (suuji.Count == 0)
            {
                UnityEditor.EditorApplication.isPlaying = false;

            }else{
                var qnum = Random.Range(0, suuji.Count);
                ban = suuji[qnum];
                Debug.Log(ban);
                Debug.Log(heycsv[ban][0]);
                Debug.Log(suuji.Count);

                xhey = float.Parse(heycsv[ban][0]);
                yhey = float.Parse(heycsv[ban][1]);
                zhey = float.Parse(heycsv[ban][2]);

                GameObject stop = Instantiate(Male, new Vector3(xhey,yhey,zhey), Quaternion.identity)　as GameObject;
                //GameObject stop = Instantiate(Male, new Vector3(0, 0, 0), Quaternion.identity)　as GameObject;
                List1.Add(stop);
                Destroy(stop, 3.0f);

                suuji.RemoveAt(qnum);
            }

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
        }
        
        //ballpos.position = Vector3.MoveTowards(ballpos.position, target, speed * Time.deltaTime);
        
        

        //Debug.Log(ballpos.position);

        timeNow =  Time.realtimeSinceStartup - timeStart;
        
        outputcsv();

        heyrad = Mathf.Atan2(heypos.position_z, heypos.position_x);
        heydegree = heyrad * Mathf.Rad2Deg -90;
        if(heydegree<0){
            heydegree = heydegree + 360;
        }

        StreamWriter neckstreamWriter = new StreamWriter(neckfilepath, true, Encoding.GetEncoding("utf-8"));
        string[] neckstreamWriter1 = {ban.ToString(),timeNow.ToString(),HMD.rotation_x.ToString(),HMD.rotation_y.ToString(),HMD.rotation_z.ToString(),heydegree.ToString(), heypos.position_x.ToString(),heypos.position_y.ToString(),heypos.position_z.ToString()};
        string neckstreamWriter2 = string.Join(",", neckstreamWriter1);
        neckstreamWriter.WriteLine(neckstreamWriter2);
        neckstreamWriter.Close();

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
    }


    void outputcsv()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            for(int i = 0; i < _key.Length; i++)
            {
                if (Input.GetKeyDown(_key[i])){
                    
                    
                    if(ban == i){
                        judge = 1;
                    }else{
                        judge = 0;
                    }

                    sayx = float.Parse(heycsv[i][0]);
                    sayy = float.Parse(heycsv[i][1]);
                    sayz = float.Parse(heycsv[i][2]);
                    saypos = new Vector3 (sayx,sayy,sayz);
                    heydis = Vector3.Distance(heypos.position, saypos);

                    StreamWriter streamWriter = new StreamWriter(ansfilepath, true, Encoding.GetEncoding("utf-8"));
                    string[] streamWriter1 = {ban.ToString() ,i.ToString(),judge.ToString(),timeNow.ToString(),heydis.ToString(),heypos.position_x.ToString(),heypos.position_y.ToString(),heypos.position_z.ToString(),heycsv[i][0].ToString(),heycsv[i][1].ToString(),heycsv[i][2].ToString()};
                    string streamWriter2 = string.Join(",", streamWriter1);
                    streamWriter.WriteLine(streamWriter2);
                    streamWriter.Close();
                }
            }    
        }

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
    }
}

