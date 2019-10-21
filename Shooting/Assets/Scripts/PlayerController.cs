using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float mSpeed, mTilt;

    [SerializeField]
    private float mXMax, mXMin, mZMax, mZMin;

    private Rigidbody mRB;

    [SerializeField]
    private float mFireRate;
    private float mCurrentFireRate;
    [SerializeField]
    private Bolt mBoltPrefab;
    [SerializeField]
    private Transform mBoltPos;

    // Start is called before the first frame update
    void Start()
    {
        mRB = GetComponent<Rigidbody>();
        mCurrentFireRate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        mRB.velocity = new Vector3(horizontal, 0, vertical).normalized * mSpeed;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, mXMin, mXMax),
                                         transform.position.y,
                                         Mathf.Clamp(transform.position.z, mZMin, mZMax));
        transform.rotation = Quaternion.Euler(0, 0, mTilt * -horizontal);

        if (0 >= mCurrentFireRate && Input.GetButton("Fire1"))
        {
            Bolt newBolt = Instantiate(mBoltPrefab);
            newBolt.transform.position = mBoltPos.position;
            mCurrentFireRate = mFireRate;
        }
        mCurrentFireRate -= Time.deltaTime;
    }
}
