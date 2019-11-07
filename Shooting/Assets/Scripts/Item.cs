using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eItemType
{
    Bolt,
    Supporter,
    Fuel
}

public class Item : MonoBehaviour
{
    [SerializeField]
    private eItemType mID;
    private Rigidbody mRB;
    [SerializeField]
    private float mSpeed;
    [SerializeField]
    private Vector3 mTorque;

    private void Awake()
    {
        mRB = GetComponent<Rigidbody>();
        mRB.velocity = Vector3.back * mSpeed;
        mRB.angularDrag = 0;
        mRB.angularVelocity = mTorque;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("GetItem", mID);
            gameObject.SetActive(false);
        }
    }
}
