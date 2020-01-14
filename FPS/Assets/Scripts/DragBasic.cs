using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBasic : MonoBehaviour
{
    [SerializeField]
    private float mCameraDistance;
    private void OnMouseDrag()
    {
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mCameraDistance);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        transform.position = worldPos;
    }
    private void OnMouseEnter()
    {
        Debug.Log("Mouse Enter");
    }
    private void OnMouseDown()
    {
        Debug.Log("Mouse Down");
    }
}
