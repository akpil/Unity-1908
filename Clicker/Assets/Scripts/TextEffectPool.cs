using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffectPool : ObjPool<TextEffect>
{
    
    [SerializeField]
    private Transform mCanvas;
    protected override TextEffect GetNewObj(int id)
    {
        TextEffect newObj = Instantiate(mOriginArr[id], mCanvas);
        mPool[id].Add(newObj);
        return newObj;
    }    
}
