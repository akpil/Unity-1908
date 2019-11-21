using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBarPool : OBJPool<EnemyHPbar>
{
    public static EnemyHPBarPool Instance;
    [SerializeField]
    private Transform mCanvas;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        PoolSetUP();
    }

    protected override EnemyHPbar GetNewObj(int id)
    {
        EnemyHPbar newObj = Instantiate(mOrigin[id], mCanvas);
        mPool[id].Add(newObj);
        return newObj;
    }
}
