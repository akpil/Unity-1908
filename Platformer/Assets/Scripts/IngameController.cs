using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameController : MonoBehaviour
{
    public static IngameController Instance;
    //public static IngameController Instance { get { return mInstance; } }

    [SerializeField]
    private Transform mEnemySpawnPos;
    [SerializeField]
    private EnemyPool mEnemyPool;
    [SerializeField]
    private int mEnemySpawnCount;
    private int mCurrentEnemyCount;

    private CameraMovement mCameraMove;
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
        mCameraMove = Camera.main.GetComponent<CameraMovement>();// 추천X
        for(mCurrentEnemyCount = 0; mCurrentEnemyCount < mEnemySpawnCount; mCurrentEnemyCount++)
        {
            Enemy e = mEnemyPool.GetFromPool(Random.Range(0, 2));
            e.transform.position = mEnemySpawnPos.position;
        }
        //GameObject[] enemyArr = GameObject.FindGameObjectsWithTag("Enemy");
        //if(enemyArr.Length < mEnemySpawnCount)
        //{
        //    //Spawn enemy
        //}
    }

    public void MoveCamera()
    {
        mCameraMove.MoveCamera();
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
