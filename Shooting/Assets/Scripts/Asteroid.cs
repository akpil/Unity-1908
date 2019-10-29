using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody mRB;
    [SerializeField]
    private float mSpeed, mTorque;

    private EffectPool mEffectpool;

    private void Awake()
    {
        mRB = GetComponent<Rigidbody>();
        mRB.velocity = Vector3.back * mSpeed;
    }

    private void OnEnable()
    {
        mRB.angularVelocity = Random.onUnitSphere * mTorque;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
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
            Timer effect = mEffectpool.GetFromPool((int)eEffecttype.Asteroid);
            effect.transform.position = transform.position;
            //터지는 소리
            //점수
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }
}
