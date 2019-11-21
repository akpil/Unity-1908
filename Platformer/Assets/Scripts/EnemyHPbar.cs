using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHPbar : GaugeBar
{
    [SerializeField]
    protected TextMeshProUGUI mPointText;
    [SerializeField]
    protected GameObject mPointWindw, mBarWindow;

    public void ShowPoint(int value)
    {
        mPointText.text = value.ToString("N0");
        mBarWindow.SetActive(false);
        mPointWindw.SetActive(true);
    }
}
