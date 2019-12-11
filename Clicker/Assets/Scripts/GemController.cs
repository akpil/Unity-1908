using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    public const int MAX_GEM_COUNT = 3;
    [SerializeField]
    private int mSheetCount = 5;
    [SerializeField]
    private SpriteRenderer mGem;
    [SerializeField]
    private Sprite[] mGemSprite;
    private double mCurrentHP, mMaxHP, mPhaseBoundary;
    private int mCurrentPhase, mStartIndex;
    // Start is called before the first frame update
    void Awake()
    {
        mGemSprite = Resources.LoadAll<Sprite>("Gem");
    }

    public void GetNewGem(int id)
    {
        mStartIndex = id * mSheetCount;
        mGem.sprite = mGemSprite[mStartIndex];
        mCurrentPhase = 0;
        mCurrentHP = 0;
        mMaxHP = 100;
        mPhaseBoundary = mMaxHP * 0.2f * (mCurrentPhase + 1);
        MainUIController.Instance.ShowProgress(0);
    }

    public bool AddProgress(double value)
    {
        mCurrentHP += value;
        MainUIController.Instance.ShowProgress((float)(mCurrentHP/ mMaxHP));
        if (mCurrentHP >= mPhaseBoundary)
        {
            //next phase
            mCurrentPhase++;
            //GameController.Instance.NextImage();
            if (mCurrentPhase > 4)
            {
                //Clear
                //GameController.Instance.NextStage();
                return true;
            }
            mGem.sprite = mGemSprite[mStartIndex + mCurrentPhase];
            mPhaseBoundary = mMaxHP * 0.2f * (mCurrentPhase + 1);
        }
        return false;
    }
}
