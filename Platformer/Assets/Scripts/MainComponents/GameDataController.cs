using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataController : MonoBehaviour
{
    public static GameDataController Instance;
    [SerializeField]
    private Item[] mInfoArr;
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
        DontDestroyOnLoad(gameObject);
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
    
    public Item GetInfo(int id)
    {
        return mInfoArr[id];
    }

    public bool LevelUP(int id, int amount)
    {
        if(mInfoArr[id].Level + amount <= mInfoArr[id].MaxLevel)
        {
            mInfoArr[id].Level += amount;
            mInfoArr[id].Cost += mInfoArr[id].CostWeight * mInfoArr[id].Level;
            return true;
        }
        return false;
    }

    public void RenewElement(int id, ScrollElement elem)
    {
        elem.Renew(mInfoArr[id].Level.ToString(), mInfoArr[id].Contents, mInfoArr[id].Cost.ToString());
    }

    public void SetElement(int id, Sprite icon, ScrollElement elem)
    {
        elem.SetUP(id, icon, mInfoArr[id].Title,
                        mInfoArr[id].Level.ToString(),
                        mInfoArr[id].Contents,
                        mInfoArr[id].Cost.ToString());
    }

    //public string[] GetInfo(int id)
    //{
    //    string[] res = new string[4];
    //    res[0] = mInfoArr[id].Title;
    //    res[1] = mInfoArr[id].Contents;
    //    res[2] = mInfoArr[id].Level.ToString();
    //    res[3] = mInfoArr[id].Cost.ToString();
    //    return res;
    //}
    
}
[System.Serializable]
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