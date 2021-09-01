using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSE : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] footSteps;
    [SerializeField]
    private AudioClip getCantera;
    [SerializeField]
    private AudioClip cantera;
    [SerializeField]
    private AudioClip jump;
    [SerializeField]
    private AudioClip land;
    [SerializeField]
    private AudioClip lightMatch;
    [SerializeField]
    private AudioClip extinguishingMatch;


    [SerializeField]
    private AudioSource footStepsAudioSource;
    [SerializeField]
    private AudioSource canteraAudioSource;
    [SerializeField]
    private AudioSource audioSource;


    [SerializeField]
    private bool footStepRandomizePitch = true;
    [SerializeField]
    private float footStepPitchRange = 0.1f;
    [SerializeField]
    private float footStepPitchAdjust = 0.1f;
    [SerializeField]
    private float canteraPitchAdjust = -0.1f;


    void Start()
    {
        //ピッチ調節
        canteraAudioSource.pitch = 1.0f + canteraPitchAdjust;

    }

    private void Awake()
    {
        footStepsAudioSource = GetComponents<AudioSource>()[0];
    }

    void Update()
    {

    }
    //ジャンプ開始
    public void PlayJumpStartSE()
    {
        audioSource.pitch = 0.9f;
        audioSource.volume = 0.2f;
        audioSource.PlayOneShot(jump);
    }
    //着地
    public void PlayJumpEndSE(float landingHeight)
    {
        audioSource.volume = 0.2f + landingHeight * 0.5f;
        audioSource.pitch = 1.0f + landingHeight * 0.5f;
        audioSource.PlayOneShot(land);
    }
    //マッチ点火
    public void PlayLightMatchSE()
    {
        audioSource.pitch = 1.0f;
        audioSource.volume = 0.2f;
        audioSource.PlayOneShot(lightMatch);
    }
    //マッチ消火
    public void PlayExtinguishingMatchSE()
    {
        audioSource.pitch = 1.0f;
        audioSource.volume = 0.2f;
        audioSource.PlayOneShot(extinguishingMatch);
    }


    //カンテラ取得
    public void PlayCanteraGetSE()
    {
        audioSource.pitch = 1.0f;
        audioSource.volume = 0.2f;
        audioSource.PlayOneShot(getCantera);
    }

    //カンテラ移動
    public void PlayCanteraMoveSE()
    {
        if (!canteraAudioSource.isPlaying)
        {
            canteraAudioSource.PlayOneShot(cantera);
        }
    }
    //カンテラ移動停止
    public void StopCanteraMoveSE()
    {
        canteraAudioSource.Stop();
    }

    //足音
    public void PlayFootStepsSE()
    {
        if (!footStepsAudioSource.isPlaying)
        {
            if (footStepRandomizePitch)
            {
                footStepsAudioSource.pitch = (1.0f + footStepPitchAdjust) + Random.Range(-footStepPitchRange, footStepPitchRange);
            }
            //音を鳴らす
            footStepsAudioSource.PlayOneShot(footSteps[Random.Range(0, footSteps.Length)]);
        }
       
    }
    //足音停止
    public void StopFootStepsSE()
    {
        footStepsAudioSource.Stop();
    }

}
