using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetting : MonoBehaviour
{
    private PlayerController mPlayer;
    private Bolt mBolt;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            mPlayer = playerObj.GetComponent<PlayerController>();
        }
        else
        {
            mPlayer = null;
        }
        mBolt = GetComponent<Bolt>();
    }

    private void OnEnable()
    {
        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds gap = new WaitForSeconds(.5f);
        while (true)
        {
            yield return gap;
            if (mPlayer != null && mPlayer.gameObject.activeInHierarchy)
            {
                mBolt.SetTarget(mPlayer.transform.position);
            }
        }
    }
}
