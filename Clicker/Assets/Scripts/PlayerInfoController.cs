using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoController : DataLoader
{
    public static PlayerInfoController Instance;
#pragma warning disable 0649
    [SerializeField]
    private PlayerInfo[] mInfos;
    [SerializeField]
    private UIElement mElementPrefab;
    [SerializeField]
    private Transform mScrollTarget;
    private List<UIElement> mElementList;

    private bool mbLoaded;
    public bool bLoaded { get { return mbLoaded; } }
    [SerializeField]
    private SkillButton[] mSkillArr;
#pragma warning restore
    [SerializeField]
    private int[] mLevelArr;
    [SerializeField]
    private float[] mCooltimeArr;
    //public int[] LevelArr
    //{
    //    get
    //    {
    //        int[] arr = new int[mInfos.Length];
    //        for(int i = 0; i < arr.Length; i++)
    //        {
    //            arr[i] = mInfos[i].Level;
    //        }
    //        return arr;
    //    }
    //}
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
        Debug.Log("sdfsdfs");
        LoadJsonData(out mInfos, StaticValues.PLAYER_DATA_PATH);
    }
    // Start is called before the first frame update
    void Start()
    {
        mElementList = new List<UIElement>();
        for(int i = 0; i < mInfos.Length; i++)
        {
            UIElement element = Instantiate(mElementPrefab, mScrollTarget);
            element.Init(null, i, mInfos[i].Name, mInfos[i].Contents, "Level UP", mInfos[i].Level, mInfos[i].ValueCurrent,
                        mInfos[i].CostCurrent, mInfos[i].Duration, AddLevel, mInfos[i].ValueType);
            mElementList.Add(element);
        }
        GameController.Instance.TouchPower = mInfos[0].ValueCurrent;
        mbLoaded = true;
    }
    
    public void Load(int[] levelArr, float[] coolTimeArr)
    {
        mLevelArr = levelArr;
        mCooltimeArr = coolTimeArr;
        for(int i = 0; i < mInfos.Length; i++)
        {
            mInfos[i].Level = levelArr[i];
            CalcAndShowData(i);
            if(mInfos[i].CoolTimeIndex >=0)
            {
                StartCoroutine(CooltimeWorks(i));
            }
        }
    }

    public void AddLevel(int id, int amount)
    {
        GameController.Instance.GoldConsumeCallback = () => { ApplyLevelUP(id, amount); };
        GameController.Instance.Gold -= mInfos[id].CostCurrent;
    }
    public void ApplyLevelUP(int id, int amount)
    {
        mInfos[id].Level += amount;
        mLevelArr[id] = mInfos[id].Level;
        CalcAndShowData(id);
        
    }

    public void CalcAndShowData(int id)
    {
        mInfos[id].CostCurrent = mInfos[id].CostBase * Math.Pow(mInfos[id].CostWeight, mInfos[id].Level);
        switch (mInfos[id].ValueType)
        {
            case eValueType.Expo:
                mInfos[id].ValueCurrent = mInfos[id].ValueBase * Math.Pow(mInfos[id].ValueWeight, mInfos[id].Level);
                break;
            case eValueType.Numeric:
            case eValueType.Percent:
                mInfos[id].ValueCurrent = mInfos[id].ValueBase + mInfos[id].ValueWeight * mInfos[id].Level;
                break;
            default:
                Debug.LogError("wrong value type : " + mInfos[id].ValueType);
                break;
        }
        mElementList[id].Renew(mInfos[id].Contents, "Level UP", mInfos[id].Level, mInfos[id].ValueCurrent,
                               mInfos[id].CostCurrent, mInfos[id].Duration, mInfos[id].ValueType);
        if (id == 0)
        {
            GameController.Instance.TouchPower = mInfos[id].ValueCurrent;
        }
        else if (id == 1)
        {

        }
        else if (id == 2)
        {

        }
        else if (id == 3)
        {
            GameController.Instance.CriticalRate = (float)mInfos[id].ValueCurrent;
        }
        else if (id == 4)
        {
            GameController.Instance.CriticalValue = (float)mInfos[id].ValueCurrent;
        }
        else if (id == 5)
        {
            GameController.Instance.IncomeBonusWeight = (float)mInfos[id].ValueCurrent;
        }
        else if (id == 6)
        {
            GameController.Instance.MaxHPWeight = (float)mInfos[id].ValueCurrent;
        }
        else
        {
            Debug.LogError("Wrong item id: " + id);
        }
    }


    public void ActiveSkill(int id)
    {
        mCooltimeArr[mInfos[id].CoolTimeIndex] = mInfos[id].CoolTime;
        StartCoroutine(CooltimeWorks(id));
        switch((eSkillID)mInfos[id].CoolTimeIndex)
        {
            case eSkillID.Chain:
                StartCoroutine(ChainFunction());
                break;
            case eSkillID.Overwork:
                int count = (int)Mathf.Ceil((float)mInfos[(int)eSkillID.Overwork].ValueCurrent);
                for(int i = 0; i < count; i++)
                {
                    ColleagueController.Instance.ForcedJobFinishAll();
                }
                break;
        }
    }
    private IEnumerator ChainFunction()
    {
        float duration = mInfos[(int)eSkillID.Chain].Duration;
        float OPS = mInfos[(int)eSkillID.Chain].Duration / (float)mInfos[(int)eSkillID.Chain].ValueCurrent;
        WaitForSeconds gap = new WaitForSeconds(OPS);

        while(duration > 0)
        {
            duration -= OPS;
            GameController.Instance.Touch();
            yield return gap;
        }
    }


    private IEnumerator CooltimeWorks(int id)
    {
        WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
        int CurrentIndex = mInfos[id].CoolTimeIndex;
        mSkillArr[CurrentIndex].SetVisible(true);
        while (mCooltimeArr[CurrentIndex] > 0)
        {
            mCooltimeArr[CurrentIndex] -= Time.fixedDeltaTime;
            mSkillArr[CurrentIndex].ShowCoolTime(mInfos[id].CoolTime, mCooltimeArr[CurrentIndex]);
            yield return fixedUpdate;
        }
        mSkillArr[CurrentIndex].SetVisible(false);
    }
}
[Serializable]
public class PlayerInfo
{
    public int ID;
    public string Name;
    public string Contents;
    public int IconID;
    public int Level;

    public eValueType ValueType;
    public double ValueCurrent;
    public double ValueWeight;
    public double ValueBase;

    public float CoolTime;
    public int CoolTimeIndex;
    public float Duration;

    public double CostCurrent;
    public double CostWeight;
    public double CostBase;
}
