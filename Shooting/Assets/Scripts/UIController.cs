using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text mScoreText, mRestartText, mStateText, mPlayerHPText;
    private Coroutine mAlphaAnimRoutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowPlayerHP(float current, float max)
    {
        mPlayerHPText.text = string.Format("HP: {0}/{1}", current.ToString(), max.ToString());
    }

    public void ShowRestartText(bool isActive)
    {
        mRestartText.gameObject.SetActive(isActive);
        if (isActive)
        {
            mAlphaAnimRoutine = StartCoroutine(RestartTextRoutine());
        }
        else
        {
            StopCoroutine(mAlphaAnimRoutine);
        }
    }

    private IEnumerator RestartTextRoutine()
    {
        WaitForSeconds pointOne = new WaitForSeconds(.1f);
        mRestartText.color = Color.white;
        Color colorGap = Color.black * 0.1f;
        float timer = 1;
        bool bDown = true;
        while (true)
        {
            yield return pointOne;
            if (bDown)
            {
                mRestartText.color -= colorGap;
            }
            else
            {
                mRestartText.color += colorGap;
            }
            timer -= .1f;

            if (timer <= 0)
            {
                bDown = !bDown;
                timer = 1;
            }
        }
    }

    public void ShowState(string value)
    {
        mStateText.text = value;
    }

    public void ShowScore(float score)
    {
        mScoreText.text = "Score: " + score.ToString();
    }
}
