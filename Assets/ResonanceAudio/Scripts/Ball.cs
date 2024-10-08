using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public GameObject sball;
    public List<GameObject> List1 = new List<GameObject>();
    public int t=0;
    public List<int> ballsuuji = new List<int>(){0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9,10,10,11,11,12,12,13,13,14,14,15,15,16,16,17,17,18,18,19,19,20,20,21,21,22,22,23,23};
    //public int suujicount = suuji.Count;
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
    private string anspath;
    private string ansfilepath;
    private string neckpath;
    private string neckfilepath;
    public float timeNow;
    private int judge;
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
        ansfilepath = anspath + @"Datas\1_ballAns_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter streamWriter = new StreamWriter(ansfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] streamWriter1 = {"Ans","Say","judge","timenow","balldis","before_position_x","before_position_y","before_position_z","before_say_position_x","before_say_position_y","before_say_position_z","after_position_x","after_position_y","after_position_z","after_say_position_x","after_say_position_y","after_say_position_z"};
        string streamWriter2 = string.Join(",", streamWriter1);
        streamWriter.WriteLine(streamWriter2);
        streamWriter.Close();

        neckfilepath = neckpath + @"Datas\1_ballneck_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter neckstreamWriter = new StreamWriter(neckfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] neckstreamWriter1 = {"Ans","timenow","HMD_rotation_x","HMD_rotation_y","HMD_rotation_z","balldeg","ball_position_x", "ball_position_y","ball_position_z"};
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

        ballrad = Mathf.Atan2(ballpos.position_z, ballpos.position_x);
        balldeg = ballrad * Mathf.Rad2Deg-90;
        if(balldeg<0){
            balldeg = balldeg + 360;
        }

        StreamWriter neckstreamWriter = new StreamWriter(neckfilepath, true, Encoding.GetEncoding("utf-8"));
        string[] neckstreamWriter1 = {ballban.ToString(),timeNow.ToString(),HMD.rotation_x.ToString(),HMD.rotation_y.ToString(),HMD.rotation_z.ToString(),balldeg.ToString(), ballpos.position_x.ToString(),ballpos.position_y.ToString(),ballpos.position_z.ToString()};
        string neckstreamWriter2 = string.Join(",", neckstreamWriter1);
        neckstreamWriter.WriteLine(neckstreamWriter2);
        neckstreamWriter.Close();
    }


    void outputcsv()
    {

        if (Input.anyKeyDown)
        {
            for(int j = 0; j < _key.Length; j++)
            {
                if (Input.GetKeyDown(_key[j])){
                    
                    
                    if(ballban == j){
                        judge = 1;
                    }else{
                        judge = 0;
                    }

                    ballsayx = float.Parse(ballcsv[j][0]);
                    ballsayy = float.Parse(ballcsv[j][1]);
                    ballsayz = float.Parse(ballcsv[j][2]);
                    ballsaypos = new Vector3 (ballsayx,ballsayy,ballsayz);
                    balldis = Vector3.Distance(beforeball, ballsaypos);

                    StreamWriter streamWriter = new StreamWriter(ansfilepath, true, Encoding.GetEncoding("utf-8"));
                    string[] streamWriter1 = {ballban.ToString() ,j.ToString(),judge.ToString(),timeNow.ToString(),balldis.ToString(),xball.ToString(), yball.ToString(), zball.ToString(),ballcsv[j][0].ToString(),ballcsv[j][1].ToString(),ballcsv[j][2].ToString(), tarcsv[ballban][0], tarcsv[ballban][1], tarcsv[ballban][2], tarcsv[j][0],tarcsv[j][1],tarcsv[j][2]};
                    string streamWriter2 = string.Join(",", streamWriter1);
                    streamWriter.WriteLine(streamWriter2);
                    streamWriter.Close();
                }
            }    
        }
    }
}
