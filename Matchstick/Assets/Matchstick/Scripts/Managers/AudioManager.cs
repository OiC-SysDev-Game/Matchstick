using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public float GetVolume(AudioGroupName name)
    {
        float ret = 0;
        audioMixer.GetFloat(name.ToString(), out ret);
        return ret;
    }

    public void SetVolume(AudioGroupName name, float value)
    {
        audioMixer.SetFloat(name.ToString(), value);
    }
}

//AudioMixer‚Ì’l‚ğæ“¾‚·‚é—p‚Ì—ñ‹“Œ^
//¦AudioMixer‘¤‚Ì•¶š—ñ‚Æˆê’v‚³‚¹‚Ä‚¨‚­‚±‚Æ
public enum AudioGroupName
{
    Master,
    BGM,
    SE
}
