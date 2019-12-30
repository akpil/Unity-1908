using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private float mTime;
#pragma warning restore

    private void OnEnable()
    {
        StartCoroutine(TimeOut());
    }

    private IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(mTime);
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
