using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    private Rigidbody mRB;
    [SerializeField]
    private float mSpeed;
    // Start is called before the first frame update
    void Start()
    {
        mRB = GetComponent<Rigidbody>();
        mRB.velocity = transform.forward * mSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
