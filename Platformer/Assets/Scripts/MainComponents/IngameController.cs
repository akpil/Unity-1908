﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private int mScore;

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
        mScore = 0;
        UIController.Instance.ShowScore(mScore);
        for (mCurrentEnemyCount = 0; mCurrentEnemyCount < mEnemySpawnCount; mCurrentEnemyCount++)
        {
            Enemy e = mEnemyPool.GetFromPool(Random.Range(0, 2));
            e.transform.position = mEnemySpawnPos.position;
        }
        StartCoroutine(EnemySpawn());
    }

    private Coroutine mSpawnRoutine;
    private IEnumerator EnemySpawn()
    {
        WaitForSeconds five = new WaitForSeconds(5);
        while(mCurrentEnemyCount < mEnemySpawnCount)
        {
            yield return five;
            Enemy e = mEnemyPool.GetFromPool(Random.Range(0, 2));
            e.transform.position = mEnemySpawnPos.position;
            mCurrentEnemyCount++;
        }
        mSpawnRoutine = null;
    }
    
    public void AddScore(int value)
    {
        mScore += value;
        UIController.Instance.ShowScore(mScore);
        mCurrentEnemyCount--;
        if(mSpawnRoutine == null)
        {
            mSpawnRoutine = StartCoroutine(EnemySpawn());
        }
    }

    public void ShowPlayerHP(float cur, float max)
    {
        UIController.Instance.ShowHP(cur, max);
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }
    }
}
