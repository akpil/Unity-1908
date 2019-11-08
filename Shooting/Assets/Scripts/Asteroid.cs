using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody mRB;
    [SerializeField]
    private float mSpeed, mTorque, mColDamage;

    [SerializeField]
    private float mMaxHP;
    private float mCurrentHp;

    private EffectPool mEffectpool;

    private SoundController mSoundController;

    private GameController mGameController;

    private void Awake()
    {
        mRB = GetComponent<Rigidbody>();
        mRB.velocity = Vector3.back * mSpeed;
        mGameController = GameObject.FindGameObjectWithTag("GameController").
                                     GetComponent<GameController>();
    }

    private void OnEnable()
    {
        mRB.angularVelocity = Random.onUnitSphere * mTorque;
        mCurrentHp = mMaxHP;
    }

    public void Hit(float value)
    {
        mCurrentHp -= value;
        if (mCurrentHp <= 0)
        {
            if (mEffectpool == null)
            {
                mEffectpool = GameObject.FindGameObjectWithTag("EffectPool").
                                        GetComponent<EffectPool>();
            }
            Timer effect = mEffectpool.GetFromPool((int)eEffecttype.Enmey);
            effect.transform.position = transform.position;

            if (mSoundController == null)
            {
                mSoundController = GameObject.FindGameObjectWithTag("SoundController").
                                            GetComponent<SoundController>();
            }
            mSoundController.PlayEffectSound((int)eSoundType.ExpEnem);

            mGameController.AddScore(10);

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("Hit", mColDamage);
        }
    }
}
