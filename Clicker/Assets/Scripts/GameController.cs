using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private double mGold;
    private int mStage;
    [SerializeField]
    private GemController mGem;

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
        int id = Random.Range(0, GemController.MAX_GEM_COUNT);
        mGem.GetNewGem(id);
    }

    public void Touch()
    {
        if(mGem.AddProgress(1))
        {
            int id = Random.Range(0, GemController.MAX_GEM_COUNT);
            mGem.GetNewGem(id);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
