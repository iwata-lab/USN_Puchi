using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using Random = UnityEngine.Random;
using Valve.VR;

public class walk : MonoBehaviour
{
    public Transform player; // プレイヤーオブジェクトのTransform
    /*
    private Vector3 TrackerPosision;//トラッカーの位置座標格納用
    private Quaternion TrackerRotationQ; //トラッカーの回転座標格納用（クォータニオン）
    private Vector3 TrackerRotation;  //トラッカーの回転座標格納用（オイラー角）
    private Vector3 trakergoalposision;
    private Vector3 trakergoalrotationanswer;
    private Vector3 trakergoalrotation;
    private Vector3 trakerballposision;
    private Vector3 trakerballrotation;
     //トラッカーのpose情報を取得するためにtracker1という関数にSteamVR_Actions.default_Poseを固定
    private SteamVR_Action_Pose tracker = SteamVR_Actions.default_Pose;
    */
      // Viveトラッカーに対応するSteamVRアクション
    public SteamVR_Action_Pose poseAction;

    // 使用するデバイス（トラッカー）の入力ソース
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any; // 任意のトラッカー


    private KATXRWalker katxrWalker;
    public Vector3 resetPosition = new Vector3(0, 0, 0); // リセットする位置
    
    public float pauseDuration = 5f; // 5秒の待機時間

    public Transform hmdTransform;
    public GameObject Male;
    public GameObject sball;
    public List<GameObject> List1 = new List<GameObject>();
    public int t=0;
    public List<int> suuji = new List<int>(){0,0,0,1,1,1,2,2,2,3,3,3,4,4,4,5,5,5,6,6,6,7,7,7};
    public List<int> ballsuuji = new List<int>(){0,0,0,1,1,1,2,2,2,3,3,3,4,4,4,5,5,5,6,6,6,7,7,7};//パターン0-7を三回ずつ計21回
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
    public float boycount;
    public float boyave;
    public float boyallcount;
    public float boyanstimeall;
    public float boyanstimeave;
    public float ballcount;
    public float ballave;
    public float ballallcount;
    public float ballanstimeall;
    public float ballanstimeave;
    public float spacecount;
    public float spaceave;
    public float spaceallcount;
    public float spaceanstimeall;
    public float spaceanstimeave;
    public float xhey;
    public float yhey;
    public float zhey;
    public int ballban;
    public float xball;
    public float yball;
    public float zball;
    public float xbody;
    public float ybody;
    public float zbody;
    public float xdis;
    public float ydis;
    public float zdis;
    public float xdisall;
    public float ydisall;
    public float zdisall;
    public float xdisave;
    public float ydisave;
    public float zdisave;

    private StreamWriter sw;
    
    public float timeStart;
    public float timeboy;
    public float timeball;
    public float timespace;
    public float timewalk;
    public float timewalkall;
    public float timewalkave;
    public string anspath;
    public string ansfilepath;
    public string spaceanspath;
    public string spaceansfilepath;
    public string neckpath;
    public string neckfilepath;
    private string ballanspath;
    private string ballansfilepath;
    private string ballneckpath;
    private string ballneckfilepath;
    public string walkpath;
    public string walkfilepath;
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
    public Vector3 answersaypos;
    private float answersayx;
    private float answersayy;
    private float answersayz;
    private float answerx;
    private float answery;
    private float answerz;
    private float ballrad;
    private float balldeg;
    private float balldis;
    private float answerdis;
    public Vector3 ballsaypos;
    public Vector3 answerpos;
    private float ballsayx;
    private float ballsayy;
    private float ballsayz;
    private float rotationdisall ;
    private float distanceall;
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
     private KeyCode[] answerkey = new KeyCode[] 
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
     
        
         // 初期位置と回転を保存
        katxrWalker= FindObjectOfType<KATXRWalker>();
    
        timeStart = Time.realtimeSinceStartup;
        
        //heyの読み込み
        csvFile = Resources.Load("Boy") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            heycsv.Add(line.Split(',')); // , 区切りでリストに追加
        }
        

        //Ballの読み込み
        ballFile = Resources.Load("Ball") as TextAsset; // Resouces下のCSV読み込み
        StringReader ballreader = new StringReader(ballFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (ballreader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string ballline = ballreader.ReadLine(); // 一行ずつ読み込み
            ballcsv.Add(ballline.Split(',')); // , 区切りでリストに追加
        }

        tarFile = Resources.Load("Answer") as TextAsset; // Resouces下のCSV読み込み
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
        string[] streamWriter1 = {"Ans","Say","judge","correctall","correctave","timenow","timeboy","timeboyave","heydis","hey_position_x","hey_position_y","hey_position_z","say_position_x","say_position_y","say_position_z"};
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
        string[] ballstreamWriter1 = {"Ans","Say","balljudge","correctall","correctave","timenow","timeball","timeballave","balldis","before_position_x","before_position_y","before_position_z","before_say_position_x","before_say_position_y","before_say_position_z","after_position_x","after_position_y","after_position_z","after_say_position_x","after_say_position_y","after_say_position_z"};
        string ballstreamWriter2 = string.Join(",", ballstreamWriter1);
        ballstreamWriter.WriteLine(ballstreamWriter2);
        ballstreamWriter.Close();

        ballneckfilepath = ballneckpath + @"Datas\2_1_ball_neck_heyball_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter ballneckstreamWriter = new StreamWriter(ballneckfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] ballneckstreamWriter1 = {"Ans","timenow","HMD_rotation_x","HMD_rotation_y","HMD_rotation_z","balldeg","ball_position_x", "ball_position_y","ball_position_z"};
        string ballneckstreamWriter2 = string.Join(",", ballneckstreamWriter1);
        ballneckstreamWriter.WriteLine(ballneckstreamWriter2);
        ballneckstreamWriter.Close();

        spaceansfilepath = spaceanspath + @"Datas\2_1_space_Ans_heyball_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter spacestreamWriter = new StreamWriter( spaceansfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] spacestreamWriter1 = {"Ans","Say","judge","correctall","correctave","timenow","timespace","timespaceave","spacedis","space_position_x","space_position_y","space_position_z","space_position_x","space_position_y","space_position_z"};
        string spacestreamWriter2 = string.Join(",", spacestreamWriter1);
        spacestreamWriter.WriteLine(spacestreamWriter2);
        spacestreamWriter.Close();

        walkfilepath = walkpath + @"Datas\2_1_walk_Ans_heyball_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter walkstreamWriter = new StreamWriter( walkfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] walkstreamWriter1 = {"time","timeall","timeave","distance","distanceave","rotation","rotationave","dis_position_x","dis_position_y","dis_position_z","body_position_x","body_position_y","body_position_z","body_rotation","body_distance","correct_position_x","correct_position_y","correct_position_z","correct_rotation"};
        string walkstreamWriter2 = string.Join(",", walkstreamWriter1);
        walkstreamWriter.WriteLine(walkstreamWriter2);
        walkstreamWriter.Close();
        
        //isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeball+=Time.deltaTime;
        timeboy+=Time.deltaTime;
        timespace+=Time.deltaTime;
        Quaternion resetRotation=hmdTransform.rotation;
        Vector3 currentSpeed = katxrWalker.Speed;
        Debug.Log(currentSpeed);
    
        
        //TitleScore.GetComponent<TextMesh>().text = "試験を選択してください\na →　Hey\nb　→　Ball\nc　→　Guide\nd　→　Hey&Ball\ne　→　Hey&Guide\nf　→　Ball&Guide\ng　→　All";
        if (Input.GetKeyDown (KeyCode.RightArrow)){
            //heyの生成
            if (suuji.Count == 0)
            {
               UnityEditor.EditorApplication.isPlaying = false;

            }else{
               
               
                timeball=0;
                timeboy=0;
                timespace=0;
                var qnum = Random.Range(0, suuji.Count);
                ban = suuji[qnum];
                /*
                Debug.Log(ban);
                Debug.Log(heycsv[ban][0]);
                Debug.Log(suuji.Count);
                */
                xhey = float.Parse(heycsv[ban][0]);
                yhey = float.Parse(heycsv[ban][1]);
                zhey = float.Parse(heycsv[ban][2]);

                xball = float.Parse(ballcsv[ban][0]);
                yball = float.Parse(ballcsv[ban][1]);
                zball = float.Parse(ballcsv[ban][2]);
               

                GameObject stop = Instantiate(Male, new Vector3(xhey,yhey,zhey), Quaternion.identity)　as GameObject;
                //GameObject stop = Instantiate(Male, new Vector3(0, 0, 0), Quaternion.identity)　as GameObject;
                GameObject stops = Instantiate(sball, new Vector3(xball,yball,zball), Quaternion.identity)　as GameObject;

                //GameObject recog = Instantiate(Male, new Vector3(0, 0, 0), Quaternion.identity)　as GameObject;
                List1.Add(stops);
                Destroy(stops, 3.0f);
                List1.Add(stop);
                Destroy(stop, 3.0f);

                suuji.RemoveAt(qnum);
            }
                }

        if (Input.GetKeyDown (KeyCode.UpArrow)){ 
            //歩行フェーズスタート
            timewalk+=Time.deltaTime;

            GameObject stop = Instantiate(Male, new Vector3(xhey,yhey,zhey), Quaternion.identity)　as GameObject;
            //GameObject stop = Instantiate(Male, new Vector3(0, 0, 0), Quaternion.identity)　as GameObject;
            GameObject stops = Instantiate(sball, new Vector3(xball,yball,zball), Quaternion.identity)　as GameObject;
            /*List1.Add(stops);
            List1.Add(stop);
            Destroy(stop, 5.0f);
            Destroy(stops, 5.0f);*/
            
                     


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
        string[] ballneckstreamWriter1 = {ban.ToString(),timeNow.ToString(),HMD.rotation_x.ToString(),HMD.rotation_y.ToString(),HMD.rotation_z.ToString(),balldeg.ToString(), ballpos.position_x.ToString(),ballpos.position_y.ToString(),ballpos.position_z.ToString()};
        string ballneckstreamWriter2 = string.Join(",", ballneckstreamWriter1);
        ballneckstreamWriter.WriteLine(ballneckstreamWriter2);
        ballneckstreamWriter.Close();
    }


    IEnumerator WaitAndDoSomething()
    {
        // 5秒待機
        yield return new WaitForSeconds(5.0f);
        
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
                        boycount++;
                    }else{
                        judge = 0;
                    }
                    boyallcount++;
                    boyave=boycount/boyallcount;

                    boyanstimeall+=timeboy;
                    boyanstimeave=boyanstimeall/boyallcount;

                    sayx = float.Parse(heycsv[i][0]);
                    sayy = float.Parse(heycsv[i][1]);
                    sayz = float.Parse(heycsv[i][2]);
                    saypos = new Vector3 (sayx,sayy,sayz);//回答ヶ所の座標
                    heydis = Vector3.Distance(heypos.position, saypos);//誤差距離の算出

                    StreamWriter streamWriter = new StreamWriter(ansfilepath, true, Encoding.GetEncoding("utf-8"));
                    string[] streamWriter1 = {ban.ToString() ,i.ToString(),judge.ToString(),boycount.ToString(),boyave.ToString(),timeNow.ToString(),timeboy.ToString(),boyanstimeave.ToString(),heydis.ToString(),heypos.position_x.ToString(),heypos.position_y.ToString(),heypos.position_z.ToString(),heycsv[i][0].ToString(),heycsv[i][1].ToString(),heycsv[i][2].ToString()};
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
                    
                    
                    if(ban == j){
                        balljudge = 1;
                        ballcount++;
                    }else{
                        balljudge = 0;
                    }
                    ballallcount++;
                    ballave=ballcount/ballallcount;

                    ballanstimeall+=timeball;
                    ballanstimeave=ballanstimeall/ballallcount;

                    ballsayx = float.Parse(ballcsv[j][0]);
                    ballsayy = float.Parse(ballcsv[j][1]);
                    ballsayz = float.Parse(ballcsv[j][2]);
                    ballsaypos = new Vector3 (ballsayx,ballsayy,ballsayz);
                    balldis = Vector3.Distance(beforeball, ballsaypos);

                    StreamWriter ballstreamWriter = new StreamWriter(ballansfilepath, true, Encoding.GetEncoding("utf-8"));
                    string[] ballstreamWriter1 = {ban.ToString() ,j.ToString(),balljudge.ToString(),ballcount.ToString(),ballave.ToString(),timeNow.ToString(),timeball.ToString(),ballanstimeave.ToString(),balldis.ToString(),xball.ToString(), yball.ToString(), zball.ToString(),ballcsv[j][0].ToString(),ballcsv[j][1].ToString(),ballcsv[j][2].ToString()};
                    string ballstreamWriter2 = string.Join(",", ballstreamWriter1);
                    ballstreamWriter.WriteLine(ballstreamWriter2);
                    ballstreamWriter.Close();
                }
            }    
        }

        if (Input.GetKey(KeyCode.C))
        {
            for(int k = 0; k <answerkey.Length; k++)
            {
                if (Input.GetKeyDown(answerkey[k])){
                                        
                    if(ban == k){
                        judge = 1;
                        spacecount++;
                    }else{
                        judge = 0;
                    }

                    spaceallcount++;
                    spaceave=spacecount/spaceallcount;

                    spaceanstimeall+=timespace;
                    spaceanstimeave=spaceanstimeall/boyallcount;
                                    
                    answerx = float.Parse(tarcsv[ban][0]);
                    answery = float.Parse(tarcsv[ban][1]);
                    answerz = float.Parse(tarcsv[ban][2]);
                    answerpos = new Vector3 (answerx,answery,answerz);//回答ヶ所の座標
                    
                    answersayx = float.Parse(tarcsv[k][0]);
                    answersayy = float.Parse(tarcsv[k][1]);
                    answersayz = float.Parse(tarcsv[k][2]);
                    answersaypos = new Vector3 (answersayx,answersayy,answersayz);//回答ヶ所の座標
                    answerdis = Vector3.Distance(answerpos, answersaypos);//誤差距離の算出。heypos.positionを要修正

                    StreamWriter streamWriter = new StreamWriter(spaceansfilepath, true, Encoding.GetEncoding("utf-8"));
                    string[] streamWriter1 = {ban.ToString() ,k.ToString(),judge.ToString(),spacecount.ToString(),spaceave.ToString(),timeNow.ToString(),timespace.ToString(),spaceanstimeave.ToString(),answerdis.ToString(),answerx.ToString(),answery.ToString(),answerz.ToString(),tarcsv[k][0].ToString(),tarcsv[k][1].ToString(),tarcsv[k][2].ToString()};
                    string streamWriter2 = string.Join(",", streamWriter1);
                    streamWriter.WriteLine(streamWriter2);
                    streamWriter.Close();
                }
            }    
        }

        if (Input.GetKeyDown (KeyCode.DownArrow))
        {

            /*
            //位置座標を取得
            TrackerPosision= tracker.GetLocalPosition(SteamVR_Input_Sources.Waist);
            //回転座標をクォータニオンで値を受け取る
            TrackerRotationQ = tracker.GetLocalRotation(SteamVR_Input_Sources.Waist);
            //取得した値をクォータニオン → オイラー角に変換
            TrackerRotation = TrackerRotationQ.eulerAngles;
            */
            // トラッカーの位置と回転を取得
        Vector3 trackerPosition = poseAction.GetLocalPosition(inputSource);
        Quaternion trackerRotation = poseAction.GetLocalRotation(inputSource);

        // 座標をコンソールに表示
        Debug.Log("Tracker Position: " + trackerPosition);
        Debug.Log("Tracker Rotation: " + trackerRotation.eulerAngles);

        // 必要であれば、トラッカーの座標に基づいてオブジェクトを移動
        transform.position = trackerPosition;
        transform.rotation = trackerRotation;



           
            timewalkall+=timewalk;
            timewalkave=timewalkall/spaceallcount;

            //現在地の取得
            xbody = hmdTransform.position.x;
            ybody = hmdTransform.position.y;
            zbody = hmdTransform.position.z;
            Vector3 bodyrotation=hmdTransform.forward;

            float xsqu= xbody*xbody;
            float zsqu= zbody*zbody;
            float sumOfSquares = xsqu + zsqu;
            float dis = (float)Math.Sqrt(sumOfSquares);
            float distance = 9-dis;//距離誤差
            float distancex=distance;
            
            xdis = xbody-answerx;
            ydis = ybody-answery;
            zdis = zbody-answerz;          

            float ansrotation=float.Parse(tarcsv[ban][4]);
            float rodis= bodyrotation.y-ansrotation;


            xdisall += xdis;
            ydisall += ydis;
            zdisall += zdis;

            if(rodis<0){
                rodis=-rodis;
            }

            if(distancex<0){
                distancex=-distancex;
            }
            
            rotationdisall+=rodis;
            distanceall+=distancex;

            xdisave = xdisall/spaceallcount;
            ydisave = ydisall/spaceallcount;
            zdisave = zdisall/spaceallcount;
            float rotationdisave = rotationdisall/spaceallcount ;
            float distanceave = distanceall/spaceallcount;
          
            StreamWriter streamWriter = new StreamWriter(walkfilepath, true, Encoding.GetEncoding("utf-8"));
            string[] streamWriter1 = {timewalk.ToString() ,timewalkall.ToString(),timewalkave.ToString(),distance.ToString(),distanceave.ToString(),rodis.ToString(),rotationdisave.ToString(),xdis.ToString(),ydis.ToString(),zdis.ToString(),xbody.ToString(),ybody.ToString(),zbody.ToString(),bodyrotation.ToString(),dis.ToString(),xdisave.ToString(),ydisave.ToString(),zdisave.ToString(),ansrotation.ToString()};
            string streamWriter2 = string.Join(",", streamWriter1);
            streamWriter.WriteLine(streamWriter2);
            streamWriter.Close();

            timewalk=0;                    
                }

         if (Input.GetKeyDown (KeyCode.LeftArrow)){

              player.position= resetPosition;   
            }
    
            } 
        }

