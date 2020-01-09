using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController mCharacterControl;
    [SerializeField]
    private float mSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        mCharacterControl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        dir = dir.normalized * mSpeed;
        dir.y -= 9.8f;
        dir = transform.TransformDirection(dir);
        mCharacterControl.Move(dir * Time.deltaTime);


        float mouseX = Input.GetAxis("Mouse X");
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + mouseX, transform.localEulerAngles.z);
    }
}
