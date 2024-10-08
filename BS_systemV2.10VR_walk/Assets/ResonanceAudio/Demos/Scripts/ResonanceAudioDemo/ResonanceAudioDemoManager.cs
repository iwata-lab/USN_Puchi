using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
/// Class that manages the ResonanceAudioDemo scene.
/// 
public class CInfo  //随机
{
    public int id;  //抽選回数
    public DateTime time; //时间
    public CItem item;  //スポット情報
    public CInfo()
    {
        item = null;
        id = 0;
    }
    public CInfo(int i,DateTime t,CItem it)
    {
        item = it;
        id = i;
        time = t;
    }

    public string getstr() //字符串
    {
        string str = string.Format("{0},{1},{2},{3}\r\n",
               item.id, item.pos.ToString(), id,time);
        return str;
    }
}
public class CItem  //音频点
{
    public int id;  //编号
    public Vector3 pos;  //位置
    public List<CInfo> index; //ランダムリスト
    public CItem()
    {
        pos = Vector3.zero;
        index = new List<CInfo>(); 
    }
    public CItem(float x,float y,float z)
    {
        pos.x = x;
        pos.y = y;
        pos.z = z;
        index = new List<CInfo>();
    }
    public void rst() //重置
    {
        index.Clear();
    }
    public string getstr() //ストリング
    {
        StringBuilder sp = new StringBuilder();
        string str = "";
        for (int i = 0; i < index.Count; i++)
        {
            str += string.Format("{0},{1},{2},{3}\r\n",
                id, pos.ToString(), index[i].id, index[i].time);
        }
        return str;
    }
}
public class ResonanceAudioDemoManager : MonoBehaviour
{
    /// Main camera.
    public Camera mainCamera;
    /// Cube controller.
    public ResonanceAudioDemoCubeController cube;
    public string filename = "Random_Number";
    StreamWriter sw;
    int num;  //ドロー数
    public int mid;  //サンプリング番号

    void Start()
    {
        num = 5;//
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        allPos = new List<CItem>();
        allPos.Add(new CItem(0.0f, 1.5f, 2.0f));//0
        allPos.Add(new CItem(1.0f, 1.5f, 1.7f));//1
        allPos.Add(new CItem(1.7f, 1.5f, 1.0f));//2
        allPos.Add(new CItem(2.0f, 1.5f, 0.0f));//3
        allPos.Add(new CItem(1.7f, 1.5f, -1.0f));//4
        allPos.Add(new CItem(1.0f, 1.5f, -1.7f));//5
        allPos.Add(new CItem(0.0f, 1.5f, -2.0f));//6
        allPos.Add(new CItem(-1.0f, 1.5f, -1.7f));//7
        allPos.Add(new CItem(-1.7f, 1.5f, -1.0f));//8
        allPos.Add(new CItem(-2.0f, 1.5f, 0.0f));//9
        allPos.Add(new CItem(-1.7f, 1.5f, 1.0f));//10
        allPos.Add(new CItem(-1.0f, 1.5f, 1.7f));//11
        allPos.Add(new CItem(0.0f, 1.5f, 8.0f));//12
        allPos.Add(new CItem(4.0f, 1.5f, 6.9f));//13
        allPos.Add(new CItem(6.9f, 1.5f, 4.0f));//14
        allPos.Add(new CItem(8.0f, 1.5f, 0.0f));//15
        allPos.Add(new CItem(6.9f, 1.5f, -4.0f));//16
        allPos.Add(new CItem(4.0f, 1.5f, -6.9f));//17
        allPos.Add(new CItem(0.0f, 1.5f, -8.0f));//18
        allPos.Add(new CItem(-4.0f, 1.5f, -6.9f));//19
        allPos.Add(new CItem(-6.9f, 1.5f, -4.0f));//20
        allPos.Add(new CItem(-8.0f, 1.5f, 0.0f));//21
        allPos.Add(new CItem(-6.9f, 1.5f, 4.0f));//22
        allPos.Add(new CItem(-4.0f, 1.5f, 6.9f));//23
        for (int i = 0; i < allPos.Count; i++)  //编号
        {
            allPos[i].id = i;
        }
        sw = new StreamWriter(@"" + filename + ".csv", false);
        string[] s1 = { "Random_Number", "time" };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
    }
    //退出
    void Update()
    {
#if !UNITY_EDITOR
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Application.Quit();
    }
#endif  // !UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.V))
        {
            ResetAll();
           
        }
        if (delTime)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (playing)
                {
                    timer = waitTime;//进入等待时期
                    cube.GetComponent<AudioSource>().Stop();
                    playing = false;
                }
                else
                {
                    RandomPos();
                }
            }

        }
    }


    public List<CItem> allPos;

    public float showTime = 3;//再生時間
    public float waitTime = 10f;//待ち時間
    private float timer = 0;//タイマー
    public bool delTime = false;//タイマースイッチ
    public bool playing = false;//音楽再生の有無
    List<CItem> randomPos = new List<CItem>();
    void ResetAll()
    {
        //座標のリストを捨て、ランダムに使用し、バックアップをとっておく。
        randomPos.Clear();
        for (int i = 0; i < allPos.Count; i++)
        {
            allPos[i].rst();
            randomPos.Add(allPos[i]);
        }
        mid = 0;
        RandomPos();
    }

    //ランダム
    void RandomPos()
    {
        if(randomPos.Count>0)  //存在点
        {
            UnityEngine.Random.InitState((int)Time.time);
            int index= UnityEngine.Random.Range(0, randomPos.Count); //随机点
            if (randomPos[index].index.Count < num)
            {
                CItem it = randomPos[index];
                CInfo info=new CInfo(mid++,DateTime.Now, it); //情報抽出
                it.index.Add(info);  //保存
                if (it.index.Count >= num)  //指定回数に到達
                {
                    randomPos.RemoveAt(index);  
                }
                string[] str = { "" + index,
                        it.index.ToString(), "" + DateTime.Now };
                string str2 = string.Join(",", str);
                sw.WriteLine(str2);
                Debug.LogWarning(str2);
                //cube.transform.position = it.pos;//出現！！！！！
                Vector3 targetPos = new Vector3(0, 0, 0);
                cube.transform.position = Vector3.MoveTowards(it.pos, targetPos, 5.0f * Time.deltaTime);
                cube.GetComponent<AudioSource>().Play();
                playing = true;
                delTime = true;
                timer = showTime;
                Debug.Log(it.getstr());
          
            }
            else
            {
                Debug.LogError("err");
            }
        }
        else
        {
            if (delTime)  //随机结束
            {
                delTime = false;
                playing = false;
                timer = 0;
                List<CInfo> infos = getres();  //抽取信息
                int len = infos.Count;
                for (int i = 0; i < len-1; i++)  //排序
                {
                    for (int j = 0; j < len - 1-i; j++)
                    {
                        if(infos[j].id>infos[j+1].id)
                        {
                            CInfo t = infos[j];
                            infos[j] = infos[j+1];
                            infos[j + 1] = t;
                        }
                    }
                }
                    string path = string.Format("{0}_{1}_{2}_{3}_{4}_{5}.csv",
                   DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                   DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                StreamWriter cf = new StreamWriter(path,false);
                string str = "";
                for (int i = 0; i < infos.Count; i++) //保存文件
                {
                    str= infos[i].getstr();
                    //str+= "\r\n";
                    cf.Write(str);
                    Debug.LogWarning(str);
                }
                cf.Close();
                Debug.LogWarning("...");
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            sw.Close();
        }
    }
    List<CInfo> getres()
    {
        List<CInfo> infos = new List<CInfo>();
        for (int i = 0; i < allPos.Count; i++)
        {
            infos.AddRange(allPos[i].index);
        }
        return infos;
    }
    
}
