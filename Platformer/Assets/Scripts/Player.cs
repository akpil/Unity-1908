using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D mRB2D;
    private Animator mAnim;
    
    private int mJumpCount;
    [SerializeField]
    private float mSpeed;
    [SerializeField]
    private float mHP;
    private float mCurrentHP;

    // Start is called before the first frame update
    void Start()
    {
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        mJumpCount = 0;
        mCurrentHP = mHP;
    }

    public void Kill()
    {
        mAnim.SetBool(AnimHash.Dead, true);
    }

    public void Hit(float damage)
    {
        mCurrentHP -= damage;
        if (mCurrentHP <= 0)
        {
            mAnim.SetBool(AnimHash.Dead, true);
        }
    }

    public void AttackTarget(GameObject target)
    {
        target.SendMessage("Hit", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(mAnim.GetBool(AnimHash.Dead))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                mAnim.SetBool(AnimHash.Dead, false);
                mCurrentHP = mHP;
            }
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        mRB2D.velocity = new Vector2(horizontal * mSpeed, mRB2D.velocity.y);

        if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            mAnim.SetBool(AnimHash.Walk, true);
        }
        else if(horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            mAnim.SetBool(AnimHash.Walk, true);
        }
        else
        {
            mAnim.SetBool(AnimHash.Walk, false);
        }

        if (mJumpCount < 1 && Input.GetButtonDown("Jump"))
        {
            mRB2D.velocity = new Vector2(mRB2D.velocity.x, 10);
            mJumpCount++;
            //mRB2D.AddForce(Vector2.up * 300);
        }
        mAnim.SetFloat(AnimHash.Jump, mRB2D.velocity.y);

        if (Input.GetButtonDown("Fire1"))
        {
            mAnim.SetBool(AnimHash.Attack, true);
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            mAnim.SetBool(AnimHash.Attack, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.enabled && collision.gameObject.CompareTag("Ground"))
        {
            mJumpCount = 0;
        }
    }
}
