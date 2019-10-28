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
    private float mCountdown;
    // Start is called before the first frame update
    void Start()
    {
        mCountdown = mPeriod;
        StartCoroutine(SpawnHazard());
        //StartCoroutine("SpawnHazard", 10);
    }

    private IEnumerator SpawnHazard()
    {
        int currentAST, currentEnemy;
        float AstRate;
        while (true)
        {
            yield return new WaitForSeconds(mPeriod);
            currentAST = mASTSpawnCount;
            currentEnemy = mEnemySpawnCount;
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
                yield return new WaitForSeconds(.5f);
            }
            for (int i = 0; i < currentAST; i++)
            {
                Asteroid ast = mAstPool.GetFromPool(Random.Range(0, 3));
                ast.transform.position = new Vector3(Random.Range(-5.5f, 5.5f), 0, 16);
                yield return new WaitForSeconds(.5f);
            }
            for (int i = 0; i < currentEnemy; i++)
            {
                Enemy enemy = mEnemyPool.GetFromPool();
                enemy.transform.position = new Vector3(Random.Range(-5.5f, 5.5f), 0, 16);
                yield return new WaitForSeconds(.5f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
