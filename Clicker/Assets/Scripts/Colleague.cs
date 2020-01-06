using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colleague : MonoBehaviour
{
    private Rigidbody2D mRB2D;
#pragma warning disable 0649
    [SerializeField]
    private float mSpeed;
    [SerializeField]
    private Transform mEffectPos;
    private Animator mAnim;
#pragma warning restore
    private int mID;

    private void Awake()
    {
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    public void Init(int id, float period)
    {
        mID = id;
        StartCoroutine(Movement());
        StartCoroutine(Function(period));
    }

    public void ForcedJobFinish()
    {
        ColleagueController.Instance.JobFinish(mID, mEffectPos.position);
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
                mAnim.SetBool(StaticValues.Move, false);
            }
            else
            {
                mRB2D.velocity = transform.right * -mSpeed;
                mAnim.SetBool(StaticValues.Move, true);
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
            ColleagueController.Instance.JobFinish(mID, mEffectPos.position);
        }
    }
}
