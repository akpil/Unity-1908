using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image mCooltimeImage;
    [SerializeField]
    private Text mCooltimeText;
#pragma warning restore

    public void ShowCoolTime(float CooltimeBase, float CooltimeCurrent)
    {
        mCooltimeImage.fillAmount = CooltimeCurrent / CooltimeBase;
        float min = Mathf.Floor(CooltimeCurrent / 60);
        float sec = CooltimeCurrent % 60;        
        mCooltimeText.text = string.Format("{0} : {1}", min.ToString("00"), sec.ToString("00"));
    }

    public void SetVisible(bool visible)
    {
        mCooltimeImage.gameObject.SetActive(visible);
    }
}
