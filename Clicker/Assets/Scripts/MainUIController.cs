using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    public static MainUIController Instance;
    private static int mUIMoveHash = Animator.StringToHash("Move");
    [SerializeField]
    private Animator[] mWindowAnims;
    [SerializeField]
    private GaugeBar mProgressBar;

    private void Awake()
    {
        if(Instance == null)
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

    public void ShowProgress(float progress)
    {
        mProgressBar.ShowGaugeBar(progress);
    }

    public void MoveWindow(int id)
    {
        mWindowAnims[id].SetTrigger(mUIMoveHash);
    }
}
