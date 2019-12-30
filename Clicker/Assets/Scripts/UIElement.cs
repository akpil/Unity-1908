using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image mIcon;
    [SerializeField]
    private Text mNameText, mLevelText, mContentsText, mPurchaseText, mCostText;
    [SerializeField]
    private Button mPurchaseButton;
#pragma warning restore
    private int mID;

    public void Init(Sprite icon, int id, string name,
                     string contents, string purchaseText,
                     int level, double value, double cost, double time,
                     StaticValues.TwoIntPramCallback callback, eValueType valueType = eValueType.Expo)
    {
        mIcon.sprite = icon;
        mID = id;
        mNameText.text = name;
        mPurchaseButton.onClick.AddListener(()=> { callback(mID, 1); });
        Renew(contents, purchaseText, level, value, cost, time, valueType);
    }
    public void Renew(string contents, string purchaseText, int level, double value, double cost, double time, eValueType elemType = eValueType.Expo)
    {
        mLevelText.text = "LV. " + level.ToString();
        string valueStr = "";
        switch(elemType)
        {
            case eValueType.Percent:
                valueStr = (value*100).ToString("N0") + "%";
                break;
            case eValueType.Numeric:
            case eValueType.Expo:
                valueStr = UnitBuilder.GetUnitStr(value);
                break;
            default:
                Debug.LogError("Wrong value type: " + elemType);
                break;
        }

        mContentsText.text = string.Format(contents, valueStr, time.ToString("N1"));
        mCostText.text = UnitBuilder.GetUnitStr(cost);
        mPurchaseText.text = purchaseText;
    }
}
