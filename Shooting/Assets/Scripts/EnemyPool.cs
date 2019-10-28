using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : OBJPool<Enemy>
{
    [SerializeField]
    private BoltPool mEnemyBoltPool;

    protected override Enemy MakeNewInstance(int id)
    {
        Enemy newObj = Instantiate(mOrigin[id]);
        newObj.SetBoltPool(mEnemyBoltPool);
        mPool[id].Add(newObj);
        return newObj;
    }
}
