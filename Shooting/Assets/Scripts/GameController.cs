using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private float mScore;
    [SerializeField]
    private UIController mUIControl;
    [SerializeField]
    private PlayerController mPlayer;
    [SerializeField]
    private int mStartLife;
    private int mCurrentLife;
    private bool mbRestart;
    [SerializeField]
    private ItemPool mItemPool;

    [Header("Hazard")]
    [SerializeField]
    private AsteroidPool mAstPool;
    [SerializeField]
    private EnemyPool mEnemyPool;
    [SerializeField]
    private Boss mBoss;
    [SerializeField]
    private float mPeriod;
    [SerializeField]
    private int mASTSpawnCount, mEnemySpawnCount;
    private Coroutine mHazardRoutine;
    private int mRoundCount;
    private float mCountdown;
    // Start is called before the first frame update
    void Start()
    {
        mbRestart = false;
        mScore = 0;
        mUIControl.ShowScore(mScore);
        mRoundCount = 0;
        mCountdown = mPeriod;
        mCurrentLife = mStartLife;
        mHazardRoutine = StartCoroutine(SpawnHazard());
    }

    public void AddScore(float amount)
    {
        mScore += amount;
        mUIControl.ShowScore(mScore);
    }

    public void GameOver()
    {
        mCurrentLife--;
        mUIControl.LooseLife(mCurrentLife);
        if (mCurrentLife > 0)
        {
            mPlayer.transform.position = Vector3.zero;
            mPlayer.gameObject.SetActive(true);
            return;
        }
        mbRestart = true;
        mUIControl.ShowState("Game Over!");
        mUIControl.ShowRestartText(true);
        StopCoroutine(mHazardRoutine);
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
        //mPlayer.gameObject.SetActive(true);
        //mPlayer.transform.position = Vector3.zero;

        //mScore = 0;
        //mUIControl.ShowScore(mScore);

        //mHazardRoutine = StartCoroutine(SpawnHazard());

        //mUIControl.ShowRestartText(false);
        //mUIControl.ShowState("");
        //mbRestart = false;
    }

    private void Update()
    {
        if (mbRestart && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
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

            while (currentAST > 0 && currentEnemy > 0)
            {
                float rate = Random.Range(0, 1f);

                if (rate < AstRate) //운석 생성
                {
                    Asteroid ast = mAstPool.GetFromPool(Random.Range(0, 3));
                    ast.transform.position = new Vector3(Random.Range(-5.5f, 5.5f), 0, 16);
                    currentAST--;
                }
                else // 적생성
                {
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
            Item item = mItemPool.GetFromPool(Random.Range(2, 3));
            item.transform.position = new Vector3(Random.Range(-5.5f, 5.5f), 0, 16);

            if (mRoundCount % 5 == 0)
            {
                mBoss.transform.position = new Vector3(0, 0, 20);
                mBoss.gameObject.SetActive(true);
                //StopCoroutine(mHazardRoutine);
                while (mBoss.IsAlive)
                {
                    yield return pointFive;
                }
            }
        }
    }
}
