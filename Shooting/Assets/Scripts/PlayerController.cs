using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float mSpeed, mTilt;

    [SerializeField]
    private float mXMax, mXMin, mZMax, mZMin;

    private Rigidbody mRB;

    [SerializeField]
    private float mFireRate;
    private float mCurrentFireRate;
    [SerializeField]
    private BoltPool mPool;
    [SerializeField]
    private Transform mBoltPos;
    [SerializeField]
    private float mBoltGap;
    [SerializeField]
    private int mBoltCount = 1;
    [SerializeField]
    private bool mSupporterFlag;
    [SerializeField]
    private GameObject[] mSupporterArr;
    [SerializeField]
    private Transform[] mSupporterBoltPosArr;

    [SerializeField]
    private BombPool mBombPool;
    [SerializeField]
    private int mBombCount;

    private EffectPool mEffectpool;

    private SoundController mSoundController;

    private GameController mGameControl;

    // Start is called before the first frame update
    void Start()
    {
        mRB = GetComponent<Rigidbody>();
        mCurrentFireRate = 0;
        mSoundController = GameObject.FindGameObjectWithTag("SoundController").
                                      GetComponent<SoundController>();
        mGameControl = GameObject.FindGameObjectWithTag("GameController").
                                  GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        mRB.velocity = new Vector3(horizontal, 0, vertical).normalized * mSpeed;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, mXMin, mXMax),
                                         transform.position.y,
                                         Mathf.Clamp(transform.position.z, mZMin, mZMax));
        transform.rotation = Quaternion.Euler(0, 0, mTilt * -horizontal);

        if (0 >= mCurrentFireRate && Input.GetButton("Fire1"))
        {
            Fire();
            mCurrentFireRate = mFireRate;
        }
        mCurrentFireRate -= Time.deltaTime;

        if (mBombCount > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            Bomb bomb = mBombPool.GetFromPool();
            bomb.transform.position = mBoltPos.position;
            mBombCount--;
        }
    }

    public void GetItem(eItemType type)
    {
        switch (type)
        {
            case eItemType.Bolt:
                mBoltCount++;
                break;
            case eItemType.Supporter:
                mSupporterFlag = true;
                for (int i = 0; i < mSupporterArr.Length; i++)
                {
                    mSupporterArr[i].SetActive(true);
                }
                break;
            default:
                Debug.LogError("Wrong item type " + type);
                break;
        }
    }

    private void Fire()
    {
        float startX = (1 - mBoltCount) / 2f * mBoltGap;
        Vector3 pos = mBoltPos.position;
        pos.x += startX;
        for (int i = 0; i < mBoltCount; i++)
        {
            Bolt newBolt = mPool.GetFromPool();
            newBolt.transform.position = pos;
            pos.x += mBoltGap;
        }

        if (mSupporterFlag)
        {
            for (int i = 0; i < mSupporterBoltPosArr.Length; i++)
            {
                Bolt newBolt = mPool.GetFromPool();
                newBolt.transform.position = mSupporterBoltPosArr[i].position;
            }
        }

        mSoundController.PlayEffectSound((int)eSoundType.FirePlayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (mEffectpool == null)
            {
                mEffectpool = GameObject.FindGameObjectWithTag("EffectPool").
                                        GetComponent<EffectPool>();
            }
            Timer effect = mEffectpool.GetFromPool((int)eEffecttype.Player);
            effect.transform.position = transform.position;

            mSoundController.PlayEffectSound((int)eSoundType.ExpPlayer);

            mGameControl.GameOver();

            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
