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
            Bolt newBolt = mPool.GetFromPool();
            newBolt.transform.position = mBoltPos.position;
            mCurrentFireRate = mFireRate;
            mSoundController.PlayEffectSound((int)eSoundType.FirePlayer);
        }
        mCurrentFireRate -= Time.deltaTime;
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
