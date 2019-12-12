using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colleague : MonoBehaviour
{
    private Rigidbody2D mRB2D;
    [SerializeField]
    private float mSpeed;
    private Animator mAnim;
    private void Awake()
    {
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private IEnumerator Movement()
    {
        WaitForSeconds moveTime = new WaitForSeconds(1);
        while(true)
        {
            int dir = Random.Range(0, 2);
            if(dir == 0) // see left side
            {
                transform.rotation = Quaternion.identity;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            int moveOrStay = Random.Range(0, 2);
            if(moveOrStay == 0)
            {
                mRB2D.velocity = Vector2.zero;
                mAnim.SetBool(AnimHash.Move, false);
            }
            else
            {
                mRB2D.velocity = transform.right * -mSpeed;
                mAnim.SetBool(AnimHash.Move, true);
            }

            yield return moveTime;
        }
    }

    private IEnumerator Function(float time)
    {
        WaitForSeconds term = new WaitForSeconds(time);
        while(true)
        {
            yield return term;
            // run special function
        }
    }
}
