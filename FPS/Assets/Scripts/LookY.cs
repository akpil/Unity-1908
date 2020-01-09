using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - mouseY, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
