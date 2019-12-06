using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    [SerializeField]
    private GameObject[] mOriginArr;
    private List<GameObject>[] mPool;

    protected void PoolSetup()
    {
        mPool = new List<GameObject>[mOriginArr.Length];
        for(int i =0; i < mPool.Length; i++)
        {
            mPool[i] = new List<GameObject>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PoolSetup();
    }

    public GameObject GetFromPool(int id)
    {
        for(int i= 0; i < mPool[id].Count; i++)
        {
            if(!mPool[id][i].gameObject.activeInHierarchy)
            {
                mPool[id][i].gameObject.SetActive(true);
                return mPool[id][i];
            }
        }

    }
}
