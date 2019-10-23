using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJPool<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    private T[] mOrigin;
    private List<T>[] mPool;
    // Start is called before the first frame update
    void Start()
    {
        mPool = new List<T>[mOrigin.Length];
        for (int i = 0; i < mPool.Length; i++)
        {
            mPool[i] = new List<T>();
        }
    }
    public T GetFromPool(int id = 0)
    {
        for (int i = 0; i < mPool[id].Count; i++)
        {
            if (!mPool[id][i].gameObject.activeInHierarchy)
            {
                mPool[id][i].gameObject.SetActive(true);
                return mPool[id][i];
            }
        }

        T newObj = Instantiate(mOrigin[id]);
        mPool[id].Add(newObj);
        return newObj;
    }
}
