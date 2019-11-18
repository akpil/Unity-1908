using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator mAnim;
    // Start is called before the first frame update
    void Start()
    {
        mAnim = GetComponent<Animator>();
        StartCoroutine(AutoMove());
    }

    IEnumerator AutoMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            mAnim.SetBool(AnimHash.Walk, true);
            yield return new WaitForSeconds(1);
            mAnim.SetBool(AnimHash.Walk, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
