﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private Vector3 mRotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(mRotateSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GameObject controllerObj = GameObject.FindGameObjectWithTag("GameController");
            GameController controller = controllerObj.GetComponent<GameController>();
            controller.AddScore(1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Stay");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
    }
}
