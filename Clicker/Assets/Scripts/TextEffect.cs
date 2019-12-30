using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Text mText;
#pragma warning restore
    public void ShowText(string data)
    {
        mText.text = data;
    }
}
