using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float mTime;

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
