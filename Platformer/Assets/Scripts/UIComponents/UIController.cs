using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField]
    private GaugeBar mPlayerGaugeBar;

    [SerializeField]
    private TextMeshProUGUI mScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowScore(int value)
    {
        mScoreText.text = value.ToString("N0");
    }

    public void ShowHP(float cur, float max)
    {
        mPlayerGaugeBar.ShowGauge(cur, max);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
