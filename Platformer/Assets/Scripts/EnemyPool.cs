using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : OBJPool<Enemy>
{
    private void Awake()
    {
        mOrigin = Resources.LoadAll<Enemy>("Prefab/Enemy");
        PoolSetUP();
    }
}
