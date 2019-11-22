using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float mTime;
    private Coroutine mRoutine;
    private void OnEnable()
    {
        mRoutine = StartCoroutine(Timeout());
    }

    private IEnumerator Timeout()
    {
        yield return new WaitForSeconds(mTime);
        gameObject.SetActive(false);
    }

    public void StopWorking()
    {
        if (mRoutine != null)
        {
            StopCoroutine(mRoutine);
        }
    }
}
