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

    // Start is called before the first frame update
    void Start()
    {
        mRB = GetComponent<Rigidbody>();
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

    }
}
