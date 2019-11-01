using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    private Rigidbody mRB;
    [SerializeField]
    private float mSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        mRB = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        mRB.velocity = transform.forward * mSpeed;
    }

    public void SetTarget(Vector3 TargetPoint)
    {
        Vector3 dir = TargetPoint - transform.position;
        mRB.velocity = dir.normalized * mSpeed;
    }
}
