using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSoundType
{
    ExpAst,
    ExpEnem,
    ExpPlayer,
    FireEnem,
    FirePlayer
}

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource mBGM, mEffect;
    [SerializeField]
    private AudioClip[] mEffectArr;
    // Start is called before the first frame update
    void Start()
    {
    }

    //샘플
    private void Play()
    {
        //쓰지 말것
        AudioSource.PlayClipAtPoint(mEffectArr[2], Vector3.zero);
    }

    public void PlayEffectSound(int id)
    {
        mEffect.PlayOneShot(mEffectArr[id]);
    }
}
