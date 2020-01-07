using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColleagueController : DataLoader
{
    public static ColleagueController Instance;
#pragma warning disable 0649
    [SerializeField]
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
    private List<UIElement> mElementList;

    [SerializeField]
    private TextEffectPool mTextEffectPool;
    private bool mbLoaded;
    public bool bLoaded { get { return mbLoaded; } }
#pragma warning restore
    private int[] mLevelArr;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            mbLoaded = false;
        }
        else
        {
            Destroy(gameObject);
        }
        LoadJsonData(out mDataArr, StaticValues.COLLEAGUE_DATA_PATH);
    }
    // Start is called before the first frame update
    void Start()
    {
        mElementList = new List<UIElement>();
        mSpawnedList = new List<Colleague>();
        for (int i =0; i < mDataArr.Length; i++)
        {
            UIElement elem = Instantiate(mElementPrefab, mScrollTarget);
            elem.Init(mIconArr[i], i, mDataArr[i].Name, mDataArr[i].Contents, "구매",
                      mDataArr[i].Level, mDataArr[i].ValueCurrent,
                      mDataArr[i].CostCurrent, mDataArr[i].JobTime,
                      AddLevel);
            mElementList.Add(elem);
        }
        mbLoaded = true;
    }

    public void Rebirth()
    {
        for (int i = 0; i < mSpawnedList.Count; i++)
        {
            Destroy(mSpawnedList[i].gameObject);
        }
    }

    public void Load(int[] levelArr)
    {
        mLevelArr = levelArr;
        for (int i = 0; i < mDataArr.Length; i++)
        {
            mDataArr[i].Level = levelArr[i];
            CalcAndShowData(i);
            if (mDataArr[i].Level > 0)
            {
                Colleague newCol = Instantiate(mPrefabArr[i]);
                newCol.transform.position = mSpawnPos.position;
                newCol.Init(i, mDataArr[i].JobTime);
                mSpawnedList.Add(newCol);
            }
        }
    }

    public void ForcedJobFinishAll()
    {
        for(int i = 0; i < mSpawnedList.Count; i++)
        {
            mSpawnedList[i].ForcedJobFinish();
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
        mLevelArr[id] = mDataArr[id].Level;
        CalcAndShowData(id);
    }
    public void CalcAndShowData(int id)
    {
        mDataArr[id].ValueCurrent = mDataArr[id].ValueBase * Math.Pow(mDataArr[id].ValueWeight, mDataArr[id].Level);
        mDataArr[id].CostCurrent = mDataArr[id].CostBase * Math.Pow(mDataArr[id].CostWeight, mDataArr[id].Level);
        mElementList[id].Renew(mDataArr[id].Contents, "구매", mDataArr[id].Level,
                               mDataArr[id].ValueCurrent, mDataArr[id].CostCurrent, mDataArr[id].JobTime);
    }
}
[Serializable]
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