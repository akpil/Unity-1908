using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyState
{
    Idle,
    Walk,
    Attack,
    Die
}

public class Enemy : MonoBehaviour
{
    private Animator mAnim;
    private Rigidbody2D mRB2D;
    private eEnemyState mState;
    [SerializeField]
    private float mSpeed;
    private Coroutine mStateMachine;
    private Player mTarget;
    [SerializeField]
    private float mHP;
    private float mCurrentHP;
    private Timer mTimer;
    // Start is called before the first frame update
    void Awake()
    {
        mAnim = GetComponent<Animator>();
        mRB2D = GetComponent<Rigidbody2D>();
        mTimer = GetComponent<Timer>();
    }
    private void OnEnable()
    {
        mState = eEnemyState.Idle;
        mTimer.enabled = false;
        mStateMachine = StartCoroutine(AutoMove());
        mCurrentHP = mHP;
    }

    public void Hit(float damage)
    {
        mCurrentHP -= damage;
        if (mCurrentHP <= 0)
        {
            mState = eEnemyState.Die;
            StateCheck();
            StopCoroutine(mStateMachine);
        }
    }

    IEnumerator AutoMove()
    {
        WaitForSeconds one = new WaitForSeconds(1);
        while (true)
        {
            yield return one;
            StateCheck();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (mState == eEnemyState.Die)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            mState = eEnemyState.Attack;
            mTarget = collision.gameObject.GetComponent<Player>();
            StateCheck();
            StopCoroutine(mStateMachine);
            mStateMachine = StartCoroutine(AutoMove());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (mState == eEnemyState.Die)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            mTarget = null;
            mState = eEnemyState.Idle;
        }
    }

    public void StateCheck()
    {
        switch (mState)
        {
            case eEnemyState.Idle:
                mAnim.SetBool(AnimHash.Walk, false);
                mAnim.SetBool(AnimHash.Attack, false);
                mRB2D.velocity = Vector2.zero;
                mState = eEnemyState.Walk;
                break;
            case eEnemyState.Walk:
                if (Random.Range(0, 2) < 1)
                {
                    mRB2D.velocity = Vector2.right * mSpeed;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    mRB2D.velocity = Vector2.left * mSpeed;
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                mAnim.SetBool(AnimHash.Walk, true);
                mAnim.SetBool(AnimHash.Attack, false);
                mState = eEnemyState.Idle;
                break;
            case eEnemyState.Attack:
                mAnim.SetBool(AnimHash.Walk, false);
                mAnim.SetBool(AnimHash.Attack, true);

                Vector2 dir = mTarget.transform.position - transform.position;
                if (dir.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                mRB2D.velocity = Vector2.zero;
                break;
            case eEnemyState.Die:
                mAnim.SetBool(AnimHash.Dead, true);
                mAnim.SetBool(AnimHash.Walk, false);
                mAnim.SetBool(AnimHash.Attack, false);
                mRB2D.velocity = Vector2.zero;
                mTimer.enabled = true;
                break;
        }
    }
}
