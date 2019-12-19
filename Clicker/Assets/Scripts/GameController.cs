using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public AnimHash.VoidCallback GoldConsumeCallback
    { get; set; }
    private double mGold;
    public double Gold {
        get { return mGold; }
        set
        {
            if (value >= 0)
            {
                if(mGold > value)
                {
                    GoldConsumeCallback?.Invoke();
                    GoldConsumeCallback = null;
                }
                mGold = value;
                MainUIController.Instance.ShowGold(mGold);
                // UI show gold
            }
            else
            {
                //돈이 부족함
                Debug.Log("Not enough gold");
            }
        }
    }
    public double GetGGold()
    {
        return mGold;
    }
    public void SetGGold(double value)
    {
        if(mGold > value)
        {
            //consumed

        }
        mGold = value;
    }
    private int mStage;
    [SerializeField]
    private GemController mGem;

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
        MainUIController.Instance.ShowGold(0);
        int id = Random.Range(0, GemController.MAX_GEM_COUNT);
        mGem.GetNewGem(id);
    }

    public void Touch()
    {
        if(mGem.AddProgress(1))
        {
            int id = Random.Range(0, GemController.MAX_GEM_COUNT);
            mGem.GetNewGem(id);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
