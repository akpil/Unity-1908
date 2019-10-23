using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private AsteroidPool mAstPool;
    [SerializeField]
    private float mPeriod;
    [SerializeField]
    private float mSpawnCount;
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
        while (true)
        {
            //wait
            yield return new WaitForSeconds(mPeriod);
            //execute
            for (int i = 0; i < mSpawnCount; i++)
            {
                Asteroid ast = mAstPool.GetFromPool(Random.Range(0,3));
                ast.transform.position = new Vector3(Random.Range(-5.5f, 5.5f), 0, 16);
                yield return new WaitForSeconds(.5f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
