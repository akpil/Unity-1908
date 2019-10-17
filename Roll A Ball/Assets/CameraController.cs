using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 mOffset;
    private Transform mPlayerTransform;

    // Start is called before the first frame update
    void Start()
    {
        mPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        mOffset = transform.position - mPlayerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mOffset + mPlayerTransform.position;
    }
}
