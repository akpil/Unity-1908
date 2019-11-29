using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    public static LobbyUIController Instance;
    
    [SerializeField]
    private ScrollElement[] mElementArr;
    [SerializeField]
    private Sprite[] mIconArr;
    [SerializeField]
    private Button mStartMainGameButton;
    // Start is called before the first frame update
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    void Start()
    {
        for(int i = 0; i < mElementArr.Length; i++)
        {
            Item data = GameDataController.Instance.GetInfo(i);
            mElementArr[i].SetUP(i, mIconArr[i],
                                data.Title,
                                data.Contents,
                                data.Level.ToString(),
                                data.Cost.ToString());
        }
    }

    //고치기 숙제
    public void SetButtonDown(int id)
    {
        //돈확인
        //레벨확인 -> 레벨업
        if(mInfoArr[id].Level + 1 <= mInfoArr[id].MaxLevel)
        {
            mInfoArr[id].Level++;
            mInfoArr[id].Cost += mInfoArr[id].CostWeight * mInfoArr[id].Level;
        }
        //UI갱신
        mElementArr[id].Renew(mInfoArr[id].Level.ToString(),
                              mInfoArr[id].Contents,
                              mInfoArr[id].Cost.ToString());
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

