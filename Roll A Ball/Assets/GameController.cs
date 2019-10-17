using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int mScore, mClearScore;

    [SerializeField]
    private Text mScoreText, mFinishText;

    // Start is called before the first frame update
    void Start()
    {
        mScore = 0;
        mScoreText.text = "Score : 0";
        mFinishText.text = "";
    }

    public void AddScore(int value)
    {
        mScore += value;
        mScoreText.text = "Score : " + mScore.ToString();
        if (mScore >= mClearScore)
        {
            mFinishText.text = "Clear!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
