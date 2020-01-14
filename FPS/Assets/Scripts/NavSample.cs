using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavSample : MonoBehaviour
{
    private NavMeshAgent mAgent;
    [SerializeField]
    private Transform mPos1, mPos2;
    private bool mToPos1;
    // Start is called before the first frame update
    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        mToPos1 = true;
        mAgent.SetDestination(mPos2.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(mAgent.remainingDistance < .01f)
        {
            if(mToPos1)
            {
                mAgent.SetDestination(mPos1.position);
            }
            else
            {
                mAgent.SetDestination(mPos2.position);
            }
            mToPos1 = !mToPos1;
        }
        
    }
}
