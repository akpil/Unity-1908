using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    delegate void AAA();
    AAA testA;
    Dictionary<string, AAA> mDic;
    [SerializeField]
    private Button mStartMainGameButton;
    // Start is called before the first frame update
    void Start()
    {
        mStartMainGameButton.onClick.AddListener(() => { Debug.Log("sfdsfsdf"); });
        mDic = new Dictionary<string, AAA>();
        mStartMainGameButton.onClick.AddListener(Test);
        SetUP(5);
        SetUP(7);
        mStartMainGameButton.onClick.RemoveListener(Test);
        mStartMainGameButton.onClick.Invoke(); //OnClick강제실행
    }

    private void Test()
    {
        Debug.Log("Test");
    }

    private void SetUP(int Data)
    {
        mStartMainGameButton.onClick.AddListener(() => { Debug.Log(Data.ToString()); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
