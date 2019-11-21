using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameController : MonoBehaviour
{
    public static IngameController Instance;
    //public static IngameController Instance { get { return mInstance; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowPlayerHP(float cur, float max)
    {
        UIController.Instance.ShowHP(cur, max);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
