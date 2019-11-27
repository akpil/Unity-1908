using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollElement : MonoBehaviour
{
    [SerializeField]
    private Image mIcon;
    [SerializeField]
    private TextMeshProUGUI mTitleText, mLevelText, mContentsText, mCostText;
    [SerializeField]
    private Button mPurchaseButton;
    private int mID;
    private void Awake()
    {
        // only for button
        mPurchaseButton.onClick.AddListener(() => { });
    }
    public void SetUP(int id, Sprite icon, string title, string level, string contents, string cost)
    {
        mID = id;
        mIcon.sprite = icon;
        mTitleText.text = title;
        mLevelText.text = level;
        mContentsText.text = contents;
        mCostText.text = cost;
    }
    public void Renew(string level, string contents, string cost)
    {
        mLevelText.text = level;
        mContentsText.text = contents;
        mCostText.text = cost;
    }

}
