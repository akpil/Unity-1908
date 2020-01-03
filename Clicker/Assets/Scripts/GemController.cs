using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    public const int MAX_GEM_COUNT = 3;
#pragma warning disable 0649
    [SerializeField]
    private EffectPool mEffectPool;
    [SerializeField]
    private int mSheetCount = 5;
    [SerializeField]
    private SpriteRenderer mGem;
    [SerializeField]
    private Sprite[] mGemSprite;
    [SerializeField]
    private float mHPBase = 10, mHPWeight = 1.4f,
                  mRewardBase = 10, mRewardWeight = 1.5f;
    private double mCurrentHP, mMaxHP, mPhaseBoundary;
    public double CurrentHP { get { return mCurrentHP; } }
    private int mCurrentPhase, mStartIndex;

    private double mIncomeBonusWeight;
    public double IncomeBonusWeight
    {
        get { return mIncomeBonusWeight; }
        set { mIncomeBonusWeight = value; }
    }

    private double mMaxHPWeight;
    public double MaxHPWeight
    {
        get { return mMaxHPWeight; }
        set { mMaxHPWeight = value; }
    }
#pragma warning restore
    // Start is called before the first frame update
    void Awake()
    {
        mGemSprite = Resources.LoadAll<Sprite>("Gem");
    }

    public void LoadGem(int lastGemID, double currentHP)
    {
        mStartIndex = lastGemID * mSheetCount;
        mCurrentHP = currentHP;
        mMaxHP = mHPBase * Math.Pow(mHPWeight, GameController.Instance.StageNumber);
        
        MainUIController.Instance.ShowProgress(mCurrentHP, mMaxHP);

        mCurrentPhase = 0;
        while(mCurrentHP >= mPhaseBoundary)
        {
            mCurrentPhase++;
            mPhaseBoundary = mMaxHP * 0.2f * (mCurrentPhase + 1);
        }
        
        mGem.sprite = mGemSprite[mStartIndex + mCurrentPhase];
    }

    public void GetNewGem(int id)
    {
        mStartIndex = id * mSheetCount;
        mGem.sprite = mGemSprite[mStartIndex];
        mCurrentPhase = 0;
        mCurrentHP = 0;
        mMaxHP = mHPBase * Math.Pow(mHPWeight, GameController.Instance.StageNumber)*
                 (1 - mMaxHPWeight);
        mPhaseBoundary = mMaxHP * 0.2f * (mCurrentPhase + 1);
        MainUIController.Instance.ShowProgress(mCurrentHP, mMaxHP);
    }

    public bool AddProgress(double value)
    {
        mCurrentHP += value;
        MainUIController.Instance.ShowProgress(mCurrentHP, mMaxHP);
        if (mCurrentHP >= mPhaseBoundary)
        {
            //next phase
            mCurrentPhase++;
            //GameController.Instance.NextImage();
            if (mCurrentPhase > 4)
            {
                //Clear
                //GameController.Instance.NextStage();
                GameController.Instance.Gold += mRewardBase * 
                            Math.Pow(mRewardWeight, GameController.Instance.StageNumber) *
                            (1 + mIncomeBonusWeight);
                return true;
            }
            Timer effect = mEffectPool.GetFromPool((int)eEffectType.PhaseShift);
            effect.transform.position = mGem.transform.position;
            mGem.sprite = mGemSprite[mStartIndex + mCurrentPhase];
            mPhaseBoundary = mMaxHP * 0.2f * (mCurrentPhase + 1);
        }
        return false;
    }
}
