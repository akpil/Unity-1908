﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D mRB2D;
    private Animator mAnim;
    private int mWalkHash, mMeleeHash;
    private int mJumpCount;
    [SerializeField]
    private float mSpeed;
    // Start is called before the first frame update
    void Start()
    {
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        mWalkHash = Animator.StringToHash("IsWalk");
        mMeleeHash = Animator.StringToHash("IsAttack");
        mJumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        mRB2D.velocity = new Vector2(horizontal * mSpeed, mRB2D.velocity.y);

        if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            mAnim.SetBool(mWalkHash, true);
        }
        else if(horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            mAnim.SetBool(mWalkHash, true);
        }
        else
        {
            mAnim.SetBool(mWalkHash, false);
        }

        if (mJumpCount < 1 && Input.GetButtonDown("Jump"))
        {
            mRB2D.velocity += Vector2.up * 10;
            mJumpCount++;
            //mRB2D.AddForce(Vector2.up * 300);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            mAnim.SetBool(mMeleeHash, true);
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            mAnim.SetBool(mMeleeHash, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            mJumpCount = 0;
        }
    }
}
