using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    private Vector3 mLastMousePos;
    // Start is called before the first frame update
    void Start()
    {
        mLastMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseDelta = Input.mousePosition - mLastMousePos;
        mLastMousePos = Input.mousePosition;

        Vector3 XRotate = new Vector3(-mouseDelta.y, 0, 0);

        transform.Rotate(XRotate);
    }
}
