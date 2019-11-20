using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GaugeBar mPlayerGaugeBar;

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
