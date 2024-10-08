using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using Random = UnityEngine.Random;

public class Hey : MonoBehaviour
{
    public GameObject Male;
    public List<GameObject> List1 = new List<GameObject>();
    public int t=0;
    public List<int> suuji = new List<int>(){0,0,0,1,1,1,2,2,2,3,3,3,4,4,4,5,5,5,6,6,6,7,7,7,8,8,8,9,9,9,10,10,10,11,11,11,12,12,12,13,13,13,14,14,14,15,15,15};
    //public int suujicount = suuji.Count;
    TextAsset csvFile; // CSVファイル
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
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I
    };
    //[SerializeField] private Transform Male;

    // Start is called before the first frame update
    void Start()
    {
        timeStart = Time.realtimeSinceStartup;

        csvFile = Resources.Load("zahyouhey") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            heycsv.Add(line.Split(',')); // , 区切りでリストに追加
        }

        //書き込み用csv
        ansfilepath = anspath + @"Datas\1_heyAns_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter streamWriter = new StreamWriter(ansfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] streamWriter1 = {"Ans","Say","judge","timenow","heydis","hey_position_x","hey_position_y","hey_position_z","say_position_x","say_position_y","say_position_z"};
        string streamWriter2 = string.Join(",", streamWriter1);
        streamWriter.WriteLine(streamWriter2);
        streamWriter.Close();

        neckfilepath = neckpath + @"Datas\1_heyneck_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +"_"+ DateTime.Now.Hour +"_"+ DateTime.Now.Minute +"_"+ DateTime.Now.Second + ".csv";
        StreamWriter neckstreamWriter = new StreamWriter(neckfilepath, false, Encoding.GetEncoding("utf-8"));
        string[] neckstreamWriter1 = {"Ans","timenow","HMD_rotation_x","HMD_rotation_y","HMD_rotation_z","heydegree","hey_position_x", "hey_position_y","hey_position_z"};
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
            if (suuji.Count == 0)
            {
                UnityEditor.EditorApplication.isPlaying = false;

            }else{
                var qnum = Random.Range(0, suuji.Count);//0～47からランダム数
                ban = suuji[qnum];
                Debug.Log(ban);
                Debug.Log(heycsv[ban][0]);
                Debug.Log(suuji.Count);

                xhey = float.Parse(heycsv[ban][0]);//読み込んだcsvから出力
                yhey = float.Parse(heycsv[ban][1]);
                zhey = float.Parse(heycsv[ban][2]);

                GameObject stop = Instantiate(Male, new Vector3(xhey,yhey,zhey), Quaternion.identity)　as GameObject;
                //GameObject stop = Instantiate(Male, new Vector3(0, 0, 0), Quaternion.identity)　as GameObject;
                List1.Add(stop);
                Destroy(stop, 3.0f);

                suuji.RemoveAt(qnum);
            }
        }

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

        //Debug.Log(HMD.rotation_y);
    }


    void outputcsv()
    {

        if (Input.anyKeyDown)
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
    }
}
