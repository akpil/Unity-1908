using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eInteractionType
{
    Attack,
    KillArea
}

public class InteractObj : MonoBehaviour
{
    [SerializeField]
    private eInteractionType mType;
    [SerializeField]
    private int mCount, mCurrentCount;
    private Coroutine mRoutine;
    // Start is called before the first frame update
    void Start()
    {
        mCurrentCount = 0;
    }

    public void Hit(int damage)
    {
        mCurrentCount++;
        if (mRoutine == null)
        {
            mRoutine = StartCoroutine(ActivateCheck());
        }
    }

    private IEnumerator ActivateCheck()
    {
        yield return new WaitForSeconds(5);
        if (mCurrentCount == mCount)
        {
            Debug.Log("Key active");
            IngameController.Instance.MoveCamera();
        }
        else
        {
            Debug.Log("fail");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (mType)
            {
                case eInteractionType.KillArea:
                    collision.gameObject.SendMessage("Kill");
                    break;
                default:
                    break;
            }
        }
    }
}
