using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 mTargetPos;
    private Vector3 mOriginPos;
    private float mProgress;
    private Animator mAnim;
    private int mCameraMoveHash;
    // Start is called before the first frame update
    void Start()
    {
        mOriginPos = transform.position;
        mAnim = GetComponent<Animator>();
        mCameraMoveHash = Animator.StringToHash("SceneNumber");
    }

    public void MoveCamera()
    {
        mAnim.SetInteger(mCameraMoveHash, mAnim.GetInteger(mCameraMoveHash) + 1);
    }

    // Update is called once per frame
    void Update()
    {
        //mProgress += 0.25f * Time.deltaTime;
        //transform.position = Vector3.Lerp(mOriginPos, mTargetPos, mProgress);
    }
}
