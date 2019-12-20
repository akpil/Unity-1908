using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    [SerializeField]
    private Text mText;
    public void ShowText(string data)
    {
        mText.text = data;
    }
}
