using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    [SerializeField]
    private Image mGauge;

    public void SetValue(float current, float max)
    {
        mGauge.fillAmount = current / max;
    }

    public void SetColor(Color color)
    {
        mGauge.color = color;
    }
}
