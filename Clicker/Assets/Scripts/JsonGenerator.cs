using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class JsonGenerator : MonoBehaviour
{
    [SerializeField]
    private ColleagueData[] mDataArr;
    [SerializeField]
    private PlayerInfo[] mPlayerInfos;
    // Start is called before the first frame update
    void Start()
    {
        //mDataArr = new ColleagueData[3];
        //mDataArr[0] = new ColleagueData();
        //mDataArr[0].Name = "No.1";
        //mDataArr[0].Level = 0;
        //mDataArr[0].Contents = "<color=#ff0000ff>{1}초</color> 마다 <color=#0000ffff>{0}골드</color>를 획득합니다.";
        //mDataArr[0].JobTime = 1.1f;
        //mDataArr[0].JobType = eJobType.Gold;
        //mDataArr[0].ValueCurrent = 1;
        //mDataArr[0].ValueWeight = 1.08d;
        //mDataArr[0].ValueBase = 1;
        //mDataArr[0].CostCurrent = 100;
        //mDataArr[0].CostWeight = 1.2d;
        //mDataArr[0].CostBase = 100;

        //mDataArr[1] = new ColleagueData();
        //mDataArr[1].Name = "No.2";
        //mDataArr[1].Level = 0;
        //mDataArr[1].Contents = "<color=#ff0000ff>{1}초</color> 마다 한번씩 터치를 해줍니다.";
        //mDataArr[1].JobTime = 1f;
        //mDataArr[1].JobType = eJobType.Touch;
        //mDataArr[1].ValueCurrent = 0;
        //mDataArr[1].ValueWeight = 1.08d;
        //mDataArr[1].ValueBase = 1;
        //mDataArr[1].CostCurrent = 200;
        //mDataArr[1].CostWeight = 1.2d;
        //mDataArr[1].CostBase = 200;

        //mDataArr[2] = new ColleagueData();
        //mDataArr[2].Name = "No.3";
        //mDataArr[2].Level = 0;
        //mDataArr[2].Contents = "<color=#ff0000ff>{1}초</color> 마다 <color=#0000ffff>{0}골드</color>를 획득합니다.";
        //mDataArr[2].JobTime = 1.5f;
        //mDataArr[2].JobType = eJobType.Gold;
        //mDataArr[2].ValueCurrent = 2;
        //mDataArr[2].ValueWeight = 1.1d;
        //mDataArr[2].ValueBase = 2;
        //mDataArr[2].CostCurrent = 300;
        //mDataArr[2].CostWeight = 1.2d;
        //mDataArr[2].CostBase = 300;
    }

    public void GenerateColleague()
    {
        string data = JsonConvert.SerializeObject(mDataArr, Formatting.Indented);
        Debug.Log(data);
        StreamWriter writer = new StreamWriter(Application.dataPath + "/Colleague.json");
        writer.Write(data);
        writer.Close();
    }
    private void LoadColleague()
    {
        string data = Resources.Load<TextAsset>("JsonFiles/Colleague").text;
        Debug.Log(data);
        mDataArr = JsonConvert.DeserializeObject<ColleagueData[]>(data);
    }

    private void SavePlayer()
    {
        //mPlayerInfos = PlayerInfoController.Instance.Infos;
        //string data = JsonConvert.SerializeObject(mPlayerInfos, Formatting.Indented);
        //Debug.Log(data);
        //StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/JsonFiles/PlayerInfo.json");
        //writer.Write(data);
        //writer.Close();
    }

    private void LoadPlayer()
    {
        string data = Resources.Load<TextAsset>("JsonFiles/PlayerInfo").text;
        Debug.Log(data);
        mPlayerInfos = JsonConvert.DeserializeObject<PlayerInfo[]>(data);
    }

    [SerializeField]
    private Dummy[] mSampleArr;
    private void LoadSample()
    {
        string data = Resources.Load<TextAsset>("JsonFiles/test").text;
        string[] dataArr = data.Split('\n');
        Debug.Log(dataArr.Length);
        mSampleArr = new Dummy[dataArr.Length - 2];
        for(int i =0; i < mSampleArr.Length; i++)
        {
            string[] splited = dataArr[i + 1].Split(',');
            mSampleArr[i] = new Dummy();
            mSampleArr[i].id = int.Parse(splited[0]);
            mSampleArr[i].name = splited[1];
            mSampleArr[i].value = int.Parse(splited[2]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            LoadSample();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GenerateColleague();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadColleague();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SavePlayer();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadPlayer();
        }
    }
}
[System.Serializable]
public class Dummy
{
    public int id;
    public string name;
    public int value;
}