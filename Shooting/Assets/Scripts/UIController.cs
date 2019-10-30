using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text mScoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowScore(float score)
    {
        mScoreText.text = "Score: " + score.ToString();
    }
}
