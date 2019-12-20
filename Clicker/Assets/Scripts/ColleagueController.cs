using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColleagueController : MonoBehaviour
{
    public static ColleagueController Instance;
    private ColleagueData[] mDataArr;
    [SerializeField]
    private Colleague[] mPrefabArr;
    private List<Colleague> mSpawnedList;
    [SerializeField]
    private Transform mSpawnPos;

    [SerializeField]
    private Sprite[] mIconArr;

    [SerializeField]
    private UIElement mElementPrefab;
    [SerializeField]
    private Transform mScrollTarget;

    [SerializeField]
    private TextEffectPool mTextEffectPool;

    private List<UIElement> mElementList;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        mDataArr = new ColleagueData[3];
        mDataArr[0] = new ColleagueData();
        mDataArr[0].Name = "No.1";
        mDataArr[0].Level = 0;
        mDataArr[0].Contents = "<color=#ff0000ff>{1}초</color> 마다 <color=#0000ffff>{0}골드</color>를 획득합니다.";
        mDataArr[0].JobTime = 1.1f;
        mDataArr[0].JobType = eJobType.Gold;
        mDataArr[0].ValueCurrent = 1;
        mDataArr[0].ValueWeight = 1.08d;
        mDataArr[0].ValueBase = 1;
        mDataArr[0].CostCurrent = 100;
        mDataArr[0].CostWeight = 1.2d;
        mDataArr[0].CostBase = 100;

        mDataArr[1] = new ColleagueData();
        mDataArr[1].Name = "No.2";
        mDataArr[1].Level = 0;
        mDataArr[1].Contents = "<color=#ff0000ff>{1}초</color> 마다 한번씩 터치를 해줍니다.";
        mDataArr[1].JobTime = 1f;
        mDataArr[1].JobType = eJobType.Touch;
        mDataArr[1].ValueCurrent = 0;
        mDataArr[1].ValueWeight = 1.08d;
        mDataArr[1].ValueBase = 1;
        mDataArr[1].CostCurrent = 200;
        mDataArr[1].CostWeight = 1.2d;
        mDataArr[1].CostBase = 200;

        mDataArr[2] = new ColleagueData();
        mDataArr[2].Name = "No.3";
        mDataArr[2].Level = 0;
        mDataArr[2].Contents = "<color=#ff0000ff>{1}초</color> 마다 <color=#0000ffff>{0}골드</color>를 획득합니다.";
        mDataArr[2].JobTime = 1.5f;
        mDataArr[2].JobType = eJobType.Gold;
        mDataArr[2].ValueCurrent = 2;
        mDataArr[2].ValueWeight = 1.1d;
        mDataArr[2].ValueBase = 2;
        mDataArr[2].CostCurrent = 300;
        mDataArr[2].CostWeight = 1.2d;
        mDataArr[2].CostBase = 300;
    }
    // Start is called before the first frame update
    void Start()
    {
        mElementList = new List<UIElement>();
        mSpawnedList = new List<Colleague>();
        for (int i =0; i < mDataArr.Length; i++)
        {
            UIElement elem = Instantiate(mElementPrefab, mScrollTarget);
            elem.Init(null, i, mDataArr[i].Name, mDataArr[i].Contents, "구매",
                      mDataArr[i].Level, mDataArr[i].ValueCurrent,
                      mDataArr[i].CostCurrent, mDataArr[i].JobTime,
                      AddLevel);
            mElementList.Add(elem);
        }
        
    }
    public void JobFinish(int id, Vector3 pos)
    {
        ColleagueData data = mDataArr[id];
        switch(data.JobType)
        {
            case eJobType.Gold:
                GameController.Instance.Gold += data.ValueCurrent;
                TextEffect effect = mTextEffectPool.GetFromPool((int)eTextEffectType.ColleagueIncome);
                effect.ShowText(UnitBuilder.GetUnitStr(data.ValueCurrent));
                effect.transform.position = pos;
                break;
            case eJobType.Touch:
                GameController.Instance.Touch();
                break;
            default:
                Debug.LogError("Wrong job type " + data.JobType);
                break;
        }
    }

    private int mCurrentId, mCurrentAmount;
    public void AddLevel(int id, int amount)
    {
        //mCurrentId = id;
        //mCurrentAmount = amount;
        //GameController.Instance.GoldConsumeCallback = ApplyLevel;
        GameController.Instance.GoldConsumeCallback = () => { ApplyLevel2(id, amount); };
        //GameController.Instance.GoldConsumeCallback = () =>
        // {
        //     if (mDataArr[id].Level == 0)
        //     {
        //         Colleague newCol = Instantiate(mPrefabArr[id]);
        //         newCol.transform.position = mSpawnPos.position;
        //         newCol.Init(id, mDataArr[id].JobTime);
        //         mSpawnedList.Add(newCol);
        //     }
        //     mDataArr[id].Level += amount;
        //     mDataArr[id].ValueCurrent = mDataArr[id].ValueBase * Math.Pow(mDataArr[id].ValueWeight, mDataArr[id].Level);
        //     mDataArr[id].CostCurrent = mDataArr[id].CostBase * Math.Pow(mDataArr[id].CostWeight, mDataArr[id].Level);
        //     mElementList[id].Renew(mDataArr[id].Contents, "구매", mDataArr[id].Level,
        //                            mDataArr[id].ValueCurrent, mDataArr[id].CostCurrent, mDataArr[id].JobTime);
        // };
        GameController.Instance.Gold -= mDataArr[id].CostCurrent;

        
    }
    public void ApplyLevel()
    {
       
    }
    public void ApplyLevel2(int id, int amount)
    {
        if (mDataArr[id].Level == 0)
        {
            Colleague newCol = Instantiate(mPrefabArr[id]);
            newCol.transform.position = mSpawnPos.position;
            newCol.Init(id, mDataArr[id].JobTime);
            mSpawnedList.Add(newCol);
        }
        mDataArr[id].Level += amount;
        mDataArr[id].ValueCurrent = mDataArr[id].ValueBase * Math.Pow(mDataArr[id].ValueWeight, mDataArr[id].Level);
        mDataArr[id].CostCurrent = mDataArr[id].CostBase * Math.Pow(mDataArr[id].CostWeight, mDataArr[id].Level);
        mElementList[id].Renew(mDataArr[id].Contents, "구매", mDataArr[id].Level,
                               mDataArr[id].ValueCurrent, mDataArr[id].CostCurrent, mDataArr[id].JobTime);
    }
}
public class ColleagueData
{
    public string Name;
    public int Level;
    public string Contents;
    public float JobTime;
    public eJobType JobType;

    public double ValueCurrent;
    public double ValueWeight;
    public double ValueBase;

    public double CostCurrent;
    public double CostWeight;
    public double CostBase;
}
public enum eJobType
{
    Gold, Touch
}