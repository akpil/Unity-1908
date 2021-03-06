﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float mSpeed, mColDamage;
    private Rigidbody mRB;
    [SerializeField]
    private Transform mHPBarPos;
    [SerializeField]
    private GaugeBar mHPBar;
    [SerializeField]
    private float mMaxHP;
    private float mCurrentHP;

    [SerializeField]
    private BoltPool mBoltPool;
    [SerializeField]
    private Transform mBoltPos;

    private EffectPool mEffectpool;

    private SoundController mSoundController;

    private GameController mGameController;

    private void Awake()
    {
        mRB = GetComponent<Rigidbody>();
        mSoundController = GameObject.FindGameObjectWithTag("SoundController").
                                        GetComponent<SoundController>();
        mGameController = GameObject.FindGameObjectWithTag("GameController").
                                        GetComponent<GameController>();
    }

    private void OnEnable()
    {
        mRB.velocity = Vector3.back * mSpeed;
        mCurrentHP = mMaxHP;
        StartCoroutine(MovePattern());
        StartCoroutine(AutoFire());
        //InvokeRepeating("AutoFireInvoke", 0.6f, 0.6f); //쓰지마
    }

    public void SetBoltPool(BoltPool pool)
    {
        mBoltPool = pool;
    }

    public void Hit(float value)
    {
        mCurrentHP -= value;
        mHPBar = mBoltPool.EnemyHPBarPool.GetFromPool();
        mHPBar.transform.position = mHPBarPos.position;
        if (mCurrentHP <= 0)
        {
            if (mEffectpool == null)
            {
                mEffectpool = GameObject.FindGameObjectWithTag("EffectPool").
                                        GetComponent<EffectPool>();
            }
            Timer effect = mEffectpool.GetFromPool((int)eEffecttype.Enmey);
            effect.transform.position = transform.position;

            mSoundController.PlayEffectSound((int)eSoundType.ExpEnem);

            mGameController.AddScore(10);

            mHPBar.gameObject.SetActive(false);
            mHPBar = null;
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (mHPBar != null)
        {
            mHPBar.transform.position = mHPBarPos.position;
        }
    }

    private void AutoFireInvoke()
    {
        Bolt newBolt = mBoltPool.GetFromPool();
        newBolt.transform.position = mBoltPos.position;
        newBolt.transform.rotation = mBoltPos.rotation;
        mSoundController.PlayEffectSound((int)eSoundType.FireEnem);
    }

    private IEnumerator AutoFire()
    {
        WaitForSeconds fireRate = new WaitForSeconds(.6f);
        while (true)
        {
            yield return fireRate;
            Bolt newBolt = mBoltPool.GetFromPool();
            newBolt.SetTargetTag("Player");
            newBolt.transform.position = mBoltPos.position;
            newBolt.transform.rotation = mBoltPos.rotation;
            mSoundController.PlayEffectSound((int)eSoundType.FireEnem);
        }
    }

    private IEnumerator MovePattern()
    {
        while (true)
        {
            // vel = (0,0,mSpeed)
            yield return new WaitForSeconds(Random.Range(.5f, 1.5f));
            if (transform.position.x < 0)
            {
                mRB.velocity += Vector3.right * Random.Range(2, 5f);
            }
            else
            {
                mRB.velocity += Vector3.left * Random.Range(2, 5f);
            }
            // vel = (x, 0, mSpeed)
            yield return new WaitForSeconds(Random.Range(.5f, 1.5f));

            Vector3 oriVel = mRB.velocity; // oriVel = (x, 0, mSpeed)
            oriVel.x = 0; // orivel = (0,0,mSpeed)
            mRB.velocity = oriVel;

            //// (x, 0, mSpeed) -= (x, 0, 0) 
            //mRB.velocity -= new Vector3(mRB.velocity.x, 0, 0);
            //// (0, 0, mSpeed)
            //mRB.velocity = new Vector3(0, 0, mRB.velocity.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //bool bIsPlayer;
        //if (bIsPlayer = other.gameObject.CompareTag("Player") ||
        //    other.gameObject.CompareTag("Bolt"))
        //{

        //}
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("Hit", mColDamage);
        }
    }
}

