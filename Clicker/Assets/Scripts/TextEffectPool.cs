using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffectPool : ObjPool<TextEffect>
{
#pragma warning disable 0649
    [SerializeField]
    private Transform mCanvas;
#pragma warning restore
    protected override TextEffect GetNewObj(int id)
    {
        TextEffect newObj = Instantiate(mOriginArr[id], mCanvas);
        mPool[id].Add(newObj);
        return newObj;
    }    
}
