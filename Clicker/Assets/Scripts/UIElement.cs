using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour
{
    [SerializeField]
    private Image mIcon;
    [SerializeField]
    private Text mNameText, mLevelText, mContentsText, mPurchaseText, mCostText;
    [SerializeField]
    private Button mPurchaseButton;
    private int mID;

    public void Init(string name, string contents, string purchaseText,
                     int level, double value, double cost, double time,
                     AnimHash.TwoIntPramCallback callback)
    {
        mPurchaseButton.onClick.AddListener(()=> { callback(mID, 1); });
        //m10UP.onClick.AddListener(() => { callback(mID, 10); });
    }
    public void Renew(string contents, string purchaseText, int level, double value, double cost,double time)
    {
        mContentsText.text = string.Format(contents, time.ToString("N1"), value.ToString());
    }
}
