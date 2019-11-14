using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObj : MonoBehaviour
{
    [SerializeField]
    private int mCount, mCurrentCount;
    // Start is called before the first frame update
    void Start()
    {
        mCurrentCount = 0;
    }

    public void Interact()
    {
        mCurrentCount++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
