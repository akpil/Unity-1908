using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image mGaugeBarImage;
    [SerializeField]
    private Text mGaugeBarText;
#pragma warning restore
    public void ShowGaugeBar(float progress, string text)
    {
        mGaugeBarImage.fillAmount = progress;
        mGaugeBarText.text = text;
    }
}
