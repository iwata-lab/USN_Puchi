using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using Random = UnityEngine.Random;

public class Hey_Guide : MonoBehaviour
{
    public GameObject Male;
    public GameObject sball;
    public List<GameObject> List1 = new List<GameObject>();
    public int t=0;
    public List<int> suuji = new List<int>(){0,0,0,1,1,1,2,2,2,3,3,3,4,4,4,5,5,5,6,6,6,7,7,7};
    //public int suujicount = suuji.Count;
    TextAsset csvFile;//ボール
    TextAsset AnswerFile;//答え
    List<string[]> heycsv = new List<string[]>(); // CSVの中身を入れるリスト;
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
    private KeyCode[] _key = new KeyCode[] 
    {
        KeyCode.Alpha1, KeyCode.Alpha2, 
        KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, 
        KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, 
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I,
        KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K
    };

    
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
     //public int suujicount = suuji.Count;
    TextAsset guidefile; // CSVファイル
    List<string[]> guidecsv = new List<string[]>(); // CSVの中身を入れるリスト;
    List<string[]> Answercsv = new List<string[]>(); 
    
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
        
        //heyの読み込み
        csvFile = Resources.Load("Ball") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            heycsv.Add(line.Split(',')); // , 区切りでリストに追加
        }
        

        //Guideの読み込み
        guidefile = Resources.Load("Boy") as TextAsset; // Resouces下のCSV読み込み
        StringReader guidereader = new StringReader(guidefile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (guidereader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string guideline = guidereader.ReadLine(); // 一行ずつ読み込み
            guidecsv.Add(guideline.Split(',')); // , 区切りでリストに追加
        }

        AnswerFile = Resources.Load("Answer") as TextAsset; // Resouces下のCSV読み込み
        StringReader answerreader = new StringReader(AnswerFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (answerreader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string answerline = answerreader.ReadLine(); // 一行ずつ読み込み
            Answercsv.Add(answerline.Split(',')); // , 区切りでリストに追加
        }


        //書き込み用csv
        ansfilepath = anspath + @"Datas\2_2_hey_Ans_heyguide_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter streamWriter = new StreamWriter(ansfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] streamWriter1 = {"Ans","Say","judge","timenow","heydis","hey_position_x","hey_position_y","hey_position_z","say_position_x","say_position_y","say_position_z"};
        string streamWriter2 = string.Join(",", streamWriter1);
        streamWriter.WriteLine(streamWriter2);
        streamWriter.Close();

        neckfilepath = neckpath + @"Datas\2_2_hey_neck_heyguide_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter neckstreamWriter = new StreamWriter(neckfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] neckstreamWriter1 = {"Ans","timenow","HMD_rotation_x","HMD_rotation_y","HMD_rotation_z","heydegree","hey_position_x", "hey_position_y","hey_position_z"};
        string neckstreamWriter2 = string.Join(",", neckstreamWriter1);
        neckstreamWriter.WriteLine(neckstreamWriter2);
        neckstreamWriter.Close();

        guideansfilepath = guideanspath + @"Datas\2_2_guide_Ans_heyguide_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter guidestreamWriter = new StreamWriter(guideansfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] guidestreamWriter1 = {"Ans","Say","guidejudge","timenow","guidedis","guide_position_x","guide_position_y","guide_position_z","guide_say_position_x","guide_say_position_y","guide_say_position_z"};
        string guidestreamWriter2 = string.Join(",", guidestreamWriter1);
        guidestreamWriter.WriteLine(guidestreamWriter2);
        guidestreamWriter.Close();

        guideneckfilepath = guideneckpath + @"Datas\2_2_guide_neck_heyguide_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
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

                xguide = float.Parse(guidecsv[guideban][0]);
                yguide = float.Parse(guidecsv[guideban][1]);
                zguide = float.Parse(guidecsv[guideban][2]);

                GameObject recog = Instantiate(sball, new Vector3(xguide,yguide,zguide), Quaternion.identity)　as GameObject;

                //GameObject recog = Instantiate(Male, new Vector3(0, 0, 0), Quaternion.identity)　as GameObject;
                List1.Add(recog);
                Destroy(recog, 3.0f);

                GameObject stop = Instantiate(Male, new Vector3(xhey,yhey,zhey), Quaternion.identity)　as GameObject;
                //GameObject stop = Instantiate(Male, new Vector3(0, 0, 0), Quaternion.identity)　as GameObject;
                List1.Add(stop);
                Destroy(stop, 3.0f);

                suuji.RemoveAt(qnum);
            }

            //guideの生成
           
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
