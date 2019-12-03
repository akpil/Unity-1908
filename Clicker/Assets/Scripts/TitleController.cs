using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private Button mStartButton;
    [SerializeField]
    private Text mStatusText;
    // Start is called before the first frame update
    void Start()
    {
        mStartButton.onClick.AddListener(StartMainGame);
        mStatusText.text = "Touch to Start";
    }

    public void StartMainGame()
    {
        SceneManager.LoadScene(1);
    }
}
