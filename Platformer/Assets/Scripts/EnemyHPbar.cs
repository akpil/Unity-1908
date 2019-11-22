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

    protected Timer mTimer;
    private void Awake()
    {
        mTimer = GetComponent<Timer>();
    }

    private void OnEnable()
    {
        mBarWindow.SetActive(true);
        mPointWindw.SetActive(false);
        mTimer.enabled = false;
    }

    public void ShowPoint(int value)
    {
        mPointText.text = "<sprite index=0>" + value.ToString("N0");
        mBarWindow.SetActive(false);
        mPointWindw.SetActive(true);
        mTimer.enabled = true;
    }
}
