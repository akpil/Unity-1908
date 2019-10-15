using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody mRB;
    private void Awake()
    {
        Debug.Log("Awake");
    }
    private void OnEnable()
    {
        Debug.Log("On enable");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        mRB = GetComponent<Rigidbody>();
        //gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        mRB.AddForce(1, 0, 0);
        //Debug.Log("aaa");
        //Debug.LogFormat("{0}, {1}", 21, 2);
        //Debug.LogWarning("bbb");
        //Debug.LogWarningFormat("{0}bbb", 45);
        //Debug.LogError("ssss");
    }
}
