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

    [Header("HP")]
    [SerializeField]
    private float mMaxHP;
    private float mCurrentHP;
    [SerializeField]
    private UIController mUIController;

    [Header("Fuel")]
    [SerializeField]
    private GaugeBar mFuelGauge;
    private float mFuel;
    [SerializeField]
    private float mMaxFuel, mFuelSpend;

    [Header("Fire & Overheat")]
    [SerializeField]
    private float mFireRate;
    [SerializeField]
    private float mOverHeatMax, mOverHeatWeight, mCooldownWeight;
    private float mCurrentFireRate, mCurrentHeat;
    [SerializeField]
    private GaugeBar mOverHeatGauge;
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

    [Header("Bomb")]
    [SerializeField]
    private BombPool mBombPool;
    [SerializeField]
    private int mBombCount;

    private EffectPool mEffectpool;

    private SoundController mSoundController;

    private GameController mGameControl;

    // Start is called before the first frame update
    void Awake()
    {
        mRB = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        mCurrentHP = mMaxHP;
        mCurrentFireRate = 0;
        mFuel = mMaxFuel;
        mOverHeatGauge.SetValue(mCurrentHeat, mOverHeatMax);
        Color color = new Color(1, 1 - mCurrentHeat / mOverHeatMax * .8f, 0, 1);
        mOverHeatGauge.SetColor(color);
        mFuelGauge.SetValue(mFuel, mMaxFuel);
        mUIController.ShowPlayerHP(mCurrentHP, mMaxHP);
    }

    private void Start()
    {
        mSoundController = GameObject.FindGameObjectWithTag("SoundController").
                                      GetComponent<SoundController>();
        mGameControl = GameObject.FindGameObjectWithTag("GameController").
                                  GetComponent<GameController>();
    }
    public void Hit(float value)
    {
        mCurrentHP -= value;
        mUIController.ShowPlayerHP(mCurrentHP, mMaxHP);

        if (mCurrentHP <= 0)
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
            gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(horizontal, 0, vertical);
        mFuel -= dir.magnitude * Time.deltaTime;
        mRB.velocity = dir.normalized * mSpeed;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, mXMin, mXMax),
                                         transform.position.y,
                                         Mathf.Clamp(transform.position.z, mZMin, mZMax));
        transform.rotation = Quaternion.Euler(0, 0, mTilt * -horizontal);

        #region FIre
        float mHeatmax = mOverHeatMax * .8f;
        if (mCurrentHeat <= mHeatmax && 0 >= mCurrentFireRate && Input.GetButton("Fire1"))
        {
            Fire();
            mCurrentHeat += mOverHeatWeight;
            
            mCurrentFireRate = mFireRate;
        }
        mCurrentFireRate -= Time.deltaTime;
        if (mCurrentHeat >= 0)
        {
            mCurrentHeat -= mCooldownWeight * Time.deltaTime;
            mOverHeatGauge.SetValue(mCurrentHeat, mOverHeatMax);
            Color color = new Color(1, 1 - mCurrentHeat / mHeatmax, 0, 1);
            mOverHeatGauge.SetColor(color);
        }
        #endregion
        if (mBombCount > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            Bomb bomb = mBombPool.GetFromPool();
            bomb.transform.position = mBoltPos.position;
            mBombCount--;
        }

        mFuel -= mFuelSpend * Time.deltaTime;
        mFuelGauge.SetValue(mFuel, mMaxFuel);
        if (mFuel <= 0)
        {
            Debug.Log("Game over");
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
            case eItemType.Fuel:
                mFuel += 5;
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
            newBolt.SetTargetTag("Enemy");
            newBolt.transform.position = pos;
            pos.x += mBoltGap;
        }

        if (mSupporterFlag)
        {
            for (int i = 0; i < mSupporterBoltPosArr.Length; i++)
            {
                Bolt newBolt = mPool.GetFromPool();
                newBolt.SetTargetTag("Enemy");
                newBolt.transform.position = mSupporterBoltPosArr[i].position;
            }
        }

        mSoundController.PlayEffectSound((int)eSoundType.FirePlayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Give damage to target
        }
    }
}
