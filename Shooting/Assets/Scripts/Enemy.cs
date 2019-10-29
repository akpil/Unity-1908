using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float mSpeed;
    private Rigidbody mRB;

    [SerializeField]
    private BoltPool mBoltPool;
    [SerializeField]
    private Transform mBoltPos;

    private EffectPool mEffectpool;
    private void Awake()
    {
        mRB = GetComponent<Rigidbody>();        
    }

    private void OnEnable()
    {
        mRB.velocity = Vector3.back * mSpeed;
        StartCoroutine(MovePattern());
        StartCoroutine(AutoFire());
    }

    public void SetBoltPool(BoltPool pool)
    {
        mBoltPool = pool;
    }

    private IEnumerator AutoFire()
    {
        WaitForSeconds fireRate = new WaitForSeconds(.6f);
        while (true)
        {
            yield return fireRate;
            Bolt newBolt = mBoltPool.GetFromPool();
            newBolt.transform.position = mBoltPos.position;
            newBolt.transform.rotation = mBoltPos.rotation;

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
        if (other.gameObject.CompareTag("Player") ||
            other.gameObject.CompareTag("Bolt"))
        {
            if (mEffectpool == null)
            {
                mEffectpool = GameObject.FindGameObjectWithTag("EffectPool").
                                        GetComponent<EffectPool>();
            }
            Timer effect = mEffectpool.GetFromPool((int)eEffecttype.Enmey);
            effect.transform.position = transform.position;
            //터지는 소리
            //점수
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }
}

