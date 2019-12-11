using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    [SerializeField]
    private Image mGaugeBarImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowGaugeBar(float progress)
    {
        mGaugeBarImage.fillAmount = progress;
    }
}
