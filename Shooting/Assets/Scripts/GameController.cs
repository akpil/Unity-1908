using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private AsteroidPool mAstPool;
    [SerializeField]
    private EnemyPool mEnemyPool;
    [SerializeField]
    private float mPeriod;
    [SerializeField]
    private int mASTSpawnCount, mEnemySpawnCount;
    private int mRoundCount;
    private float mCountdown;
    // Start is called before the first frame update
    void Start()
    {
        mRoundCount = 0;
        mCountdown = mPeriod;
        StartCoroutine(SpawnHazard());
        //StartCoroutine("SpawnHazard", 10);
    }

    private IEnumerator SpawnHazard()
    {
        WaitForSeconds pointFive = new WaitForSeconds(.5f);
        WaitForSeconds period = new WaitForSeconds(mPeriod);
        int currentAST, currentEnemy;
        float AstRate;
        while (true)
        {
            yield return period;
            currentAST = mASTSpawnCount + mRoundCount * (mRoundCount + 1) / 2;
            currentEnemy = mEnemySpawnCount + mRoundCount/2;
            AstRate = (float)currentAST / (currentAST + currentEnemy);
            Debug.Log("ASTRate : " + AstRate);

            while (currentAST > 0 && currentEnemy > 0)
            {
                float rate = Random.Range(0, 1f);
                Debug.Log("rate : " + rate);
                if (rate < AstRate) //운석 생성
                {
                    Debug.Log("AST");
                    Asteroid ast = mAstPool.GetFromPool(Random.Range(0, 3));
                    ast.transform.position = new Vector3(Random.Range(-5.5f, 5.5f), 0, 16);
                    currentAST--;
                }
                else // 적생성
                {
                    Debug.Log("Enemy");
                    Enemy enemy = mEnemyPool.GetFromPool();
                    enemy.transform.position = new Vector3(Random.Range(-5.5f, 5.5f), 0, 16);
                    currentEnemy--;
                }
                yield return pointFive;
            }
            for (int i = 0; i < currentAST; i++)
            {
                Asteroid ast = mAstPool.GetFromPool(Random.Range(0, 3));
                ast.transform.position = new Vector3(Random.Range(-5.5f, 5.5f), 0, 16);
                yield return pointFive;
            }
            for (int i = 0; i < currentEnemy; i++)
            {
                Enemy enemy = mEnemyPool.GetFromPool();
                enemy.transform.position = new Vector3(Random.Range(-5.5f, 5.5f), 0, 16);
                yield return pointFive;
            }
            mRoundCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
