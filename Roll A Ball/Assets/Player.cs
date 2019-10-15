using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
