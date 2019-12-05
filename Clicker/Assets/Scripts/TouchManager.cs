using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    [SerializeField]
    private Camera mTouchCamera;
    [SerializeField]
    private GameObject mDummy;
 
    public Ray GenerateRay(Vector3 screenPos)
    {
        Vector3 nearPlane = mTouchCamera.ScreenToWorldPoint(
                                new Vector3(screenPos.x, screenPos.y, mTouchCamera.nearClipPlane));
        Vector3 farPlane = mTouchCamera.ScreenToWorldPoint(
                                new Vector3(screenPos.x, screenPos.y, mTouchCamera.farClipPlane));
        return new Ray(nearPlane, farPlane - nearPlane);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GenerateRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider.gameObject == gameObject)
                {
                    GameObject dummy = Instantiate(mDummy);
                    dummy.transform.position = hit.point;
                    // GameController.Touch();
                }
            }
        }
    }
}
