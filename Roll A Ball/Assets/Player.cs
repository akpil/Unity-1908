using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody mRB;

    [SerializeField]
    private float mSpeed = 10;

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
        float horiznotal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(horiznotal, 0, vertical) * mSpeed;

        //mRB.AddForce(velocity);
        mRB.velocity = velocity;
        //transform.position += velocity * Time.deltaTime;    
        //transform.Translate(velocity);
        //mRB.MovePosition(velocity);


        //Debug.Log("aaa");
        //Debug.LogFormat("{0}, {1}", 21, 2);
        //Debug.LogWarning("bbb");
        //Debug.LogWarningFormat("{0}bbb", 45);
        //Debug.LogError("ssss");
    }
}
