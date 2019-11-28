using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    public static LobbyUIController Instance;
    private Item[] mInfoArr;
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

        mInfoArr = new Item[3];
        mInfoArr[0] = new Item();
        mInfoArr[0].Title = "HP";
        mInfoArr[0].Contents = "Add HP";
        mInfoArr[0].Level = 1;
        mInfoArr[0].MaxLevel = 10;
        mInfoArr[0].Cost = 100;
        mInfoArr[0].CostWeight = 200;

        mInfoArr[1] = new Item();
        mInfoArr[1].Title = "Def";
        mInfoArr[1].Contents = "Add Def";
        mInfoArr[1].Level = 1;
        mInfoArr[1].MaxLevel = 50;
        mInfoArr[1].Cost = 200;
        mInfoArr[1].CostWeight = 250;

        mInfoArr[2] = new Item();
        mInfoArr[2].Title = "Abil";
        mInfoArr[2].Contents = "Add Abil";
        mInfoArr[2].Level = 1;
        mInfoArr[2].MaxLevel = 20;
        mInfoArr[2].Cost = 250;
        mInfoArr[2].CostWeight = 300;
    }
    void Start()
    {
        for(int i = 0; i < mElementArr.Length; i++)
        {
            mElementArr[i].SetUP(i, mIconArr[i],
                                mInfoArr[i].Title,
                                mInfoArr[i].Level.ToString(),
                                mInfoArr[i].Contents,
                                mInfoArr[i].Cost.ToString());
        }
    }

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

public class Item
{
    public int ID;
    public int IconID;
    public string Title;
    public string Contents;
    public int Level;
    public int MaxLevel;
    public int Cost;
    public int CostWeight;
}