using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBarPool: OBJPool<GaugeBar>
{
    [SerializeField]
    private Transform mCanvas;

    protected override GaugeBar MakeNewInstance(int id)
    {
        GaugeBar newObj = Instantiate(mOrigin[id], mCanvas);
        mPool[id].Add(newObj);
        return newObj;
    }
}
