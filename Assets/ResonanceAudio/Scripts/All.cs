using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using Random = UnityEngine.Random;

public class All : MonoBehaviour
{
    public GameObject Male;
    public List<GameObject> List1 = new List<GameObject>();
    public int t=0;
    private List<int> suuji = new List<int>(){0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47};
    //public int suujicount = suuji.Count;
    TextAsset csvFile;
    TextAsset allcsvFile;
    List<string[]> heycsv = new List<string[]>(); // CSVの中身を入れるリスト;
    List<string[]> allheycsv = new List<string[]>(); // CSVの中身を入れるリスト;
    public Vector3 position;
    public Quaternion rotation;
    public int ban;
    public float xhey;
    public float yhey;
    public float zhey;
    private StreamWriter sw;
    public float timeStart;
    public string anspath;
    public string ansfilepath;
    public string neckpath;
    public string neckfilepath;
    public float timeNow;
    private int judge;
    private float heyrad;
    private float heydegree;
    private float heydis;
    public Vector3 saypos;
    private float sayx;
    private float sayy;
    private float sayz;

    public GameObject sball;
    private List<int> ballsuuji = new List<int>(){0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9,10,10,11,11,12,12,13,13,14,14,15,15,16,16,17,17,18,18,19,19,20,20,21,21,22,22,23,23};
    TextAsset ballFile; // CSVファイル
    TextAsset tarFile; // CSVファイル
    List<string[]> ballcsv = new List<string[]>(); // CSVの中身を入れるリスト;
    List<string[]> tarcsv = new List<string[]>(); // CSVの中身を入れるリスト;
    TextAsset allballFile; // CSVファイル
    TextAsset alltarFile; // CSVファイル
    List<string[]> allballcsv = new List<string[]>(); // CSVの中身を入れるリスト;
    List<string[]> alltarcsv = new List<string[]>(); // CSVの中身を入れるリスト;
    public int ballban;
    public float xball;
    public float yball;
    public float zball;
    private string ballanspath;
    private string ballansfilepath;
    private string ballneckpath;
    private string ballneckfilepath;
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
    TextAsset allguidefile; // CSVファイル
    List<string[]> allguidecsv = new List<string[]>(); // CSVの中身を入れるリスト;  
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
    public List<GameObject> allguidelist = new List<GameObject>()
    {
        back8, back8, Lback2, Rfront2, Rback8, Rback8, Lback2, L2, Lback2, Lfront8, R8, R8, front2, Rback2, L2, back2, R2, Rfront8, front8, Lfront2, Rback2, Rfront8, L8, R8,
        Lback8, back2, Lback2, Lback8, back8, Rback2, Lfront2, back8, front8, Lfront8, L2, L8, Lfront8, front2, Lfront2, R8, front8, Rback8, R2, Rfront2, back2, Rfront8, Rfront2,Lback8
    };

    public string allanspath;
    public string allansfilepath;
    
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
            string tarline = tarreader.ReadLine(); // 一行ずつ読み込み
            tarcsv.Add(tarline.Split(',')); // , 区切りでリストに追加
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


        //heyの読み込み
        allcsvFile = Resources.Load("allhey") as TextAsset; // Resouces下のCSV読み込み
        StringReader allreader = new StringReader(allcsvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (allreader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string allline = allreader.ReadLine(); // 一行ずつ読み込み
            allheycsv.Add(allline.Split(',')); // , 区切りでリストに追加
        }
        
        //Ballの読み込み
        allballFile = Resources.Load("allball") as TextAsset; // Resouces下のCSV読み込み
        StringReader allballreader = new StringReader(allballFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (allballreader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string allballline = allballreader.ReadLine(); // 一行ずつ読み込み
            allballcsv.Add(allballline.Split(',')); // , 区切りでリストに追加
        }

        alltarFile = Resources.Load("alltarget") as TextAsset; // Resouces下のCSV読み込み
        StringReader alltarreader = new StringReader(alltarFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (alltarreader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = alltarreader.ReadLine(); // 一行ずつ読み込み
            alltarcsv.Add(line.Split(',')); // , 区切りでリストに追加
        }
        

        //Guideの読み込み
        allguidefile = Resources.Load("allguide") as TextAsset; // Resouces下のCSV読み込み
        StringReader allguidereader = new StringReader(allguidefile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (allguidereader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string allguideline = allguidereader.ReadLine(); // 一行ずつ読み込み
            allguidecsv.Add(allguideline.Split(',')); // , 区切りでリストに追加
        }


        //書き込み用csv
        ansfilepath = anspath + @"Datas\3_hey_Ans_All_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter streamWriter = new StreamWriter(ansfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] streamWriter1 = {"Ans","Say","judge","timenow","heydis","hey_position_x","hey_position_y","hey_position_z","say_position_x","say_position_y","say_position_z"};
        string streamWriter2 = string.Join(",", streamWriter1);
        streamWriter.WriteLine(streamWriter2);
        streamWriter.Close();

        neckfilepath = neckpath + @"Datas\3_hey_neck_All_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter neckstreamWriter = new StreamWriter(neckfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] neckstreamWriter1 = {"Ans","timenow","HMD_rotation_x","HMD_rotation_y","HMD_rotation_z","heydegree","hey_position_x", "hey_position_y","hey_position_z"};
        string neckstreamWriter2 = string.Join(",", neckstreamWriter1);
        neckstreamWriter.WriteLine(neckstreamWriter2);
        neckstreamWriter.Close();

        ballansfilepath = ballanspath + @"Datas\3_ball_Ans_All_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter ballstreamWriter = new StreamWriter(ballansfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] ballstreamWriter1 = {"Ans","Say","balljudge","timenow","balldis","before_position_x","before_position_y","before_position_z","before_say_position_x","before_say_position_y","before_say_position_z","after_position_x","after_position_y","after_position_z","after_say_position_x","after_say_position_y","after_say_position_z"};
        string ballstreamWriter2 = string.Join(",", ballstreamWriter1);
        ballstreamWriter.WriteLine(ballstreamWriter2);
        ballstreamWriter.Close();

        ballneckfilepath = ballneckpath + @"Datas\3_ball_neck_All_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter ballneckstreamWriter = new StreamWriter(ballneckfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] ballneckstreamWriter1 = {"Ans","timenow","HMD_rotation_x","HMD_rotation_y","HMD_rotation_z","balldeg","ball_position_x", "ball_position_y","ball_position_z"};
        string ballneckstreamWriter2 = string.Join(",", ballneckstreamWriter1);
        ballneckstreamWriter.WriteLine(ballneckstreamWriter2);
        ballneckstreamWriter.Close();

        guideansfilepath = guideanspath + @"Datas\3_guide_Ans_All_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter guidestreamWriter = new StreamWriter(guideansfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] guidestreamWriter1 = {"Ans","Say","guidejudge","timenow","guidedis","guide_position_x","guide_position_y","guide_position_z","guide_say_position_x","guide_say_position_y","guide_say_position_z"};
        string guidestreamWriter2 = string.Join(",", guidestreamWriter1);
        guidestreamWriter.WriteLine(guidestreamWriter2);
        guidestreamWriter.Close();

        guideneckfilepath = guideneckpath + @"Datas\3_guide_neck_All_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter guideneckstreamWriter = new StreamWriter(guideneckfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] guideneckstreamWriter1 = {"Ans","timenow","HMD_rotation_x","HMD_rotation_y","HMD_rotation_z","guidedeg","guide_position_x", "guide_position_y","guide_position_z"};
        string guideneckstreamWriter2 = string.Join(",", guideneckstreamWriter1);
        guideneckstreamWriter.WriteLine(guideneckstreamWriter2);
        guideneckstreamWriter.Close();

        allansfilepath = allanspath + @"Datas\3_Yubi_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter allstreamWriter = new StreamWriter(allansfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] allstreamWriter1 = {"timenow","HMD_rotation_x","HMD_rotation_y","HMD_rotation_z","hey_position_x","hey_position_y","hey_position_z","after_position_x","after_position_y","after_position_z","guide_position_x","guide_position_y","guide_position_z"};
        string allstreamWriter2 = string.Join(",", allstreamWriter1);
        allstreamWriter.WriteLine(allstreamWriter2);
        allstreamWriter.Close();
        
        //isStart = false;*/
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
                Debug.Log(allheycsv[ban][0]);
                Debug.Log(suuji.Count);

                //hey生成
                xhey = float.Parse(allheycsv[ban][0]);
                yhey = float.Parse(allheycsv[ban][1]);
                zhey = float.Parse(allheycsv[ban][2]);

                GameObject stop = Instantiate(Male, new Vector3(xhey,yhey,zhey), Quaternion.identity)　as GameObject;
                //GameObject stop = Instantiate(Male, new Vector3(0, 0, 0), Quaternion.identity)　as GameObject;
                
                //ball生成
                xball = float.Parse(allballcsv[ban][0]);
                yball = float.Parse(allballcsv[ban][1]);
                zball = float.Parse(allballcsv[ban][2]);
                beforeball = new Vector3(xball,yball,zball);

                target = new Vector3(float.Parse(alltarcsv[ban][0]),float.Parse(alltarcsv[ban][1]),float.Parse(alltarcsv[ban][2]));

                GameObject move = Instantiate(sball, new Vector3(xball,yball,zball), Quaternion.identity)　as GameObject;
                
                //guide生成
                xguide = float.Parse(allguidecsv[ban][0]);
                yguide = float.Parse(allguidecsv[ban][1]);
                zguide = float.Parse(allguidecsv[ban][2]);

                GameObject recog = Instantiate(allguidelist[ban], new Vector3(0f, 0f, 8f), Quaternion.identity)　as GameObject;

                //GameObject recog = Instantiate(Male, new Vector3(0, 0, 0), Quaternion.identity)　as GameObject;
                List1.Add(recog);
                Destroy(recog, 3.0f);

                List1.Add(move);
                //move.transform.position = Vector3.MoveTowards(move.transform.position, target, speed * Time.deltaTime);

                //GameObject stop = Instantiate(Male, new Vector3(0, 0, 0), Quaternion.identity)　as GameObject;
                
                //Destroy(move, 2.0f);
                //ballsuuji.RemoveAt(ballnum);
                
                List1.Add(stop);
                Destroy(stop, 3.0f);

                suuji.RemoveAt(qnum);
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
        string[] neckstreamWriter1 = {allheycsv[ban][3].ToString(),timeNow.ToString(),HMD.rotation_x.ToString(),HMD.rotation_y.ToString(),HMD.rotation_z.ToString(),heydegree.ToString(), heypos.position_x.ToString(),heypos.position_y.ToString(),heypos.position_z.ToString()};
        string neckstreamWriter2 = string.Join(",", neckstreamWriter1);
        neckstreamWriter.WriteLine(neckstreamWriter2);
        neckstreamWriter.Close();

        //ballの首角度
        ballrad = Mathf.Atan2(ballpos.position_z, ballpos.position_x);
        balldeg = ballrad * Mathf.Rad2Deg-90;
        if(balldeg<0){
            balldeg = balldeg + 360;
        }

        StreamWriter ballneckstreamWriter = new StreamWriter(ballneckfilepath, true, Encoding.GetEncoding("utf-8"));
        string[] ballneckstreamWriter1 = {allballcsv[ban][3].ToString(),timeNow.ToString(),HMD.rotation_x.ToString(),HMD.rotation_y.ToString(),HMD.rotation_z.ToString(),balldeg.ToString(), ballpos.position_x.ToString(),ballpos.position_y.ToString(),ballpos.position_z.ToString()};
        string ballneckstreamWriter2 = string.Join(",", ballneckstreamWriter1);
        ballneckstreamWriter.WriteLine(ballneckstreamWriter2);
        ballneckstreamWriter.Close();

        guiderad = Mathf.Atan2(zguide, xguide);
        guidedeg = guiderad * Mathf.Rad2Deg -90;
        if(guidedeg<0){
            guidedeg = guidedeg + 360;
        }

        StreamWriter guideneckstreamWriter = new StreamWriter(guideneckfilepath, true, Encoding.GetEncoding("utf-8"));
        string[] guideneckstreamWriter1 = {allguidecsv[ban][3].ToString(),timeNow.ToString(),HMD.rotation_x.ToString(),HMD.rotation_y.ToString(),HMD.rotation_z.ToString(),guidedeg.ToString(), xguide.ToString(), yguide.ToString(), zguide.ToString()};
        string guideneckstreamWriter2 = string.Join(",", guideneckstreamWriter1);
        guideneckstreamWriter.WriteLine(guideneckstreamWriter2);
        guideneckstreamWriter.Close();
    }


    void outputcsv()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            for(int i = 0; i < _key.Length; i++)
            {
                if (Input.GetKeyDown(_key[i])){
                    
                    
                    if(int.Parse(allheycsv[ban][3]) == i){
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
                    string[] streamWriter1 = {allheycsv[ban][3].ToString() ,i.ToString(),judge.ToString(),timeNow.ToString(),heydis.ToString(),heypos.position_x.ToString(),heypos.position_y.ToString(),heypos.position_z.ToString(),heycsv[i][0].ToString(),heycsv[i][1].ToString(),heycsv[i][2].ToString()};
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
                    
                    
                    if(int.Parse(allballcsv[ban][3]) == j){
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
                    string[] ballstreamWriter1 = {allballcsv[ban][3].ToString() ,j.ToString(),balljudge.ToString(),timeNow.ToString(),balldis.ToString(),xball.ToString(), yball.ToString(), zball.ToString(),ballcsv[j][0].ToString(),ballcsv[j][1].ToString(),ballcsv[j][2].ToString(), tarcsv[ballban][0], tarcsv[ballban][1], tarcsv[ballban][2], tarcsv[j][0],tarcsv[j][1],tarcsv[j][2]};
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
                    
                    
                    if(int.Parse(allguidecsv[ban][3]) == k){
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
                    string[] guidestreamWriter1 = {allguidecsv[ban][3].ToString() ,k.ToString(),guidejudge.ToString(),timeNow.ToString(),guidedis.ToString(), xguide.ToString(), yguide.ToString(), zguide.ToString(),guidecsv[k][0].ToString(),guidecsv[k][1].ToString(),guidecsv[k][2].ToString()};
                    string guidestreamWriter2 = string.Join(",", guidestreamWriter1);
                    guidestreamWriter.WriteLine(guidestreamWriter2);
                    guidestreamWriter.Close();
                }
            }    
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            StreamWriter allstreamWriter = new StreamWriter(allansfilepath, true, Encoding.GetEncoding("utf-8"));
            string[] allstreamWriter1 = {timeNow.ToString(),HMD.rotation_x.ToString(),HMD.rotation_y.ToString(),HMD.rotation_z.ToString(),heypos.position_x.ToString(),heypos.position_y.ToString(),heypos.position_z.ToString(),tarcsv[ballban][0], tarcsv[ballban][1], tarcsv[ballban][2],xguide.ToString(), yguide.ToString(), zguide.ToString()};
            string allstreamWriter2 = string.Join(",", allstreamWriter1);
            allstreamWriter.WriteLine(allstreamWriter2);
            allstreamWriter.Close();
        }
    }
}
