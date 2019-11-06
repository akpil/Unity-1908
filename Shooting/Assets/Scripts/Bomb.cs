using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody mRB;
    [SerializeField]
    private EffectPool mEffectPool;
    [SerializeField]
    private float mSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        mRB = GetComponent<Rigidbody>();
        mEffectPool = GameObject.FindGameObjectWithTag("EffectPool").
                                 GetComponent<EffectPool>();
    }
    private void OnEnable()
    {
        mRB.velocity = Vector3.forward * mSpeed;
    }

    private void OnDisable()
    {
        Timer effect = mEffectPool.GetFromPool((int)eEffecttype.Bomb);
        effect.transform.position = transform.position;
    }
}
