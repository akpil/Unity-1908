using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltPool : MonoBehaviour
{
    [SerializeField]
    private Bolt mOrigin;
    private List<Bolt> mPool;
    // Start is called before the first frame update
    void Start()
    {
        mPool = new List<Bolt>();
    }

    public Bolt GetFromPool()
    {
        for (int i = 0; i < mPool.Count; i++)
        {
            if (!mPool[i].gameObject.activeInHierarchy)
            {
                mPool[i].gameObject.SetActive(true);
                return mPool[i];
            }
        }
        Bolt newObj = Instantiate(mOrigin);
        mPool.Add(newObj);
        return newObj;
    }
}
