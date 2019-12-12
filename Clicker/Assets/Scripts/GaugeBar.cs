using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    [SerializeField]
    private Image mGaugeBarImage;
    [SerializeField]
    private Text mGaugeBarText;
    public void ShowGaugeBar(float progress, string text)
    {
        mGaugeBarImage.fillAmount = progress;
        mGaugeBarText.text = text;
    }
}
