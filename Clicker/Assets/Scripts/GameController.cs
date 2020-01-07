using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField]
    private PlayerSaveData mPlayer;

    public StaticValues.VoidCallback GoldConsumeCallback
    { get; set; }
    public double Gold {
        get { return mPlayer.Gold; }
        set
        {
            if (value >= 0)
            {
                if(mPlayer.Gold > value)
                {
                    GoldConsumeCallback?.Invoke();
                    GoldConsumeCallback = null;
                }
                mPlayer.Gold = value;
                MainUIController.Instance.ShowGold(mPlayer.Gold);
                // UI show gold
            }
            else
            {
                //돈이 부족함
                Debug.Log("Not enough gold");
            }
        }
    }
    //public double GetGGold()
    //{
    //    return mGold;
    //}
    //public void SetGGold(double value)
    //{
    //    if(mGold > value)
    //    {
    //        //consumed

    //    }
    //    mGold = value;
    //}

    public int StageNumber
    {
        get { return mPlayer.Stage; }
    }
#pragma warning disable 0649
    [SerializeField]
    private GemController mGem;
#pragma warning restore

    public double TouchPower
    {
        get { return mTouchPower; }
        set { mTouchPower = value; }
    }
    private double mTouchPower;
    
    public float CriticalRate
    {
        get { return mCriticalRate; }
        set { mCriticalRate = value; }
    }
    private float mCriticalRate;

    public float CriticalValue
    {
        get { return mCriticalValue; }
        set { mCriticalValue = value; }
    }
    private float mCriticalValue;

    public double IncomeBonusWeight
    {
        get { return mGem.IncomeBonusWeight; }
        set { mGem.IncomeBonusWeight = value; }
    }

    public double MaxHPWeight
    {
        get { return mGem.MaxHPWeight; }
        set { mGem.MaxHPWeight = value; }
    }

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
    }
    // Start is called before the first frame update
    void Start()
    {
        MainUIController.Instance.ShowGold(mPlayer.Gold);
        //PlayerPrefs.DeleteAll();
        Load();
        StartCoroutine(LoadGames());
        
        //mPlayer.GemID = UnityEngine.Random.Range(0, GemController.MAX_GEM_COUNT);
        //mGem.GetNewGem(mPlayer.GemID);
    }

    private IEnumerator LoadGames()
    {
        WaitForSeconds pointOne = new WaitForSeconds(.1f);
        while(!PlayerInfoController.Instance.bLoaded || 
              !ColleagueController.Instance.bLoaded)
        {
            yield return pointOne;
        }
        if(mPlayer.GemID < 0)
        {
            mPlayer.GemID = UnityEngine.Random.Range(0, GemController.MAX_GEM_COUNT);
        }
        mGem.LoadGem(mPlayer.GemID, mPlayer.GemHP);
        PlayerInfoController.Instance.Load(mPlayer.PlayerLevels, mPlayer.Cooltimes);
        ColleagueController.Instance.Load(mPlayer.ColleagueLevels);
    }

    public void Touch()
    {
        double touchPower = mTouchPower;

        float randVal = UnityEngine.Random.value;

        if(randVal <= mCriticalRate)
        {
            touchPower *= 1 + mCriticalValue;
            Debug.Log("Cirtical" +
                "!!!!!");
        }

        if (mGem.AddProgress(touchPower))
        {
            mPlayer.Stage++;
            mPlayer.GemID = UnityEngine.Random.Range(0, GemController.MAX_GEM_COUNT);
            mGem.GetNewGem(mPlayer.GemID);
        }
    }

    public void Rebirth()
    {
        mPlayer.Soul += 10 * mPlayer.Stage;

        mPlayer.Gold = 0;
        mPlayer.GemID = -1;
        mPlayer.PlayerLevels = new int[StaticValues.PLAYER_INFOS_LEGNTH];
        mPlayer.PlayerLevels[0] = 1;
        mPlayer.Cooltimes = new float[StaticValues.COOLTIME_LENGTH];
        mPlayer.ColleagueLevels = new int[StaticValues.COLLEAGUE_INFOS_LENGTH];
        mPlayer.Stage = 0;
        mPlayer.GemHP = 0;

        PlayerInfoController.Instance.Load(mPlayer.PlayerLevels, mPlayer.Cooltimes);
        ColleagueController.Instance.Rebirth();
        ColleagueController.Instance.Load(mPlayer.ColleagueLevels);
    }

    public void Save()
    {
        mPlayer.GemHP = mGem.CurrentHP;
        //mPlayer.PlayerLevels = PlayerInfoController.Instance.LevelArr;
        //mPlayer.ColleagueLevels = ColleagueController.Instance.LevelArr;

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();

        formatter.Serialize(stream, mPlayer);

        string data = Convert.ToBase64String(stream.GetBuffer());
        Debug.Log(data);
        PlayerPrefs.SetString("Player", data);
        stream.Close();
    }
    public void Load()
    {
        string data = PlayerPrefs.GetString("Player");
        if (!string.IsNullOrEmpty(data))
        {
            Debug.Log(data);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));

            mPlayer = (PlayerSaveData)formatter.Deserialize(stream);
            stream.Close();
        }
        else
        {
            mPlayer = new PlayerSaveData();
            mPlayer.GemID = -1;
            mPlayer.PlayerLevels = new int[StaticValues.PLAYER_INFOS_LEGNTH];
            mPlayer.PlayerLevels[0] = 1;
            mPlayer.Cooltimes = new float[StaticValues.COOLTIME_LENGTH];
            mPlayer.ColleagueLevels = new int[StaticValues.COLLEAGUE_INFOS_LENGTH];
        }
        FixSavedData();
    }

    private void FixSavedData()
    {
        if (mPlayer.PlayerLevels == null)
        {
            mPlayer.PlayerLevels = new int[StaticValues.PLAYER_INFOS_LEGNTH];
        }
        else if (mPlayer.PlayerLevels.Length < StaticValues.PLAYER_INFOS_LEGNTH)
        {
            int[] temp = new int[StaticValues.PLAYER_INFOS_LEGNTH];
            for(int i =0; i < mPlayer.PlayerLevels.Length; i++)
            {
                temp[i] = mPlayer.PlayerLevels[i];
            }
            mPlayer.PlayerLevels = temp;
        }

        if (mPlayer.Cooltimes == null)
        {
            mPlayer.Cooltimes = new float[StaticValues.COOLTIME_LENGTH];
        }
        else if (mPlayer.Cooltimes.Length < StaticValues.COOLTIME_LENGTH)
        {
            float[] temp = new float[StaticValues.COOLTIME_LENGTH];
            for(int i =0; i < mPlayer.Cooltimes.Length; i++)
            {
                temp[i] = mPlayer.Cooltimes[i];
            }
            mPlayer.Cooltimes = temp;
        }

        if(mPlayer.ColleagueLevels == null)
        {
            mPlayer.ColleagueLevels = new int[StaticValues.COLLEAGUE_INFOS_LENGTH];
        }
        else if(mPlayer.ColleagueLevels.Length < StaticValues.COLLEAGUE_INFOS_LENGTH)
        {
            int[] temp = new int[StaticValues.COLLEAGUE_INFOS_LENGTH];
            for (int i = 0; i < mPlayer.ColleagueLevels.Length; i++)
            {
                temp[i] = mPlayer.ColleagueLevels[i];
            }
            mPlayer.ColleagueLevels = temp;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Save();
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            Load();
            mGem.LoadGem(mPlayer.GemID, mPlayer.GemHP);
        }

    }
}
