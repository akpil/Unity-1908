using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GaugeBar : MonoBehaviour
{
    [SerializeField]
    private Image mBar;
    [SerializeField]
    private TextMeshProUGUI mValueText;

    public void ShowGauge(float currnet, float max)
    {
        mBar.fillAmount = currnet / max;
        mValueText.text = string.Format("{0}/{1}", currnet.ToString("N0"), max.ToString("N0"));
        //mValueText.text = mBar.fillAmount.ToString("P0");
    }
}
