using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public const string VolumeDataFileName = "audio_volume.data";
    public readonly AudioGroup[] saveAudioGroups =
        {
            AudioGroup.BGM,
            AudioGroup.SE
        };

    [SerializeField] private AudioMixer audioMixer;

    private static AudioManager instance;

    public static AudioManager Instance{
        get {
            if(instance == null)
            {
                instance = (AudioManager)FindObjectOfType(typeof(AudioManager));
                if (instance == null)
                {
                    Debug.LogError(typeof(AudioManager) + "がシーンに存在しません");
                }
            }
            return instance;
        }
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public void Start()
    {
        //音量のデータの読み込み
        Load();
    }

    public void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    /// <summary>
    /// 現在の音量のデータをファイルに保存する関数
    /// </summary>
    public void Save()
    {
        VolumeDataListWrapper wrapper = new VolumeDataListWrapper();
        foreach (var audioGroup in saveAudioGroups)
        {
            VolumeData volumeData = new VolumeData(audioGroup, GetVolume(audioGroup));
            wrapper.volumeDataList.Add(volumeData);
        }
        string data = JsonUtility.ToJson(wrapper);
        FileManager.Save(VolumeDataFileName, data);
    }

    /// <summary>
    /// 音量のデータをファイルから読み取り適用する関数
    /// </summary>
    public void Load()
    {
        string data = FileManager.Load(VolumeDataFileName);
        VolumeDataListWrapper wrapper = JsonUtility.FromJson<VolumeDataListWrapper>(data);
        foreach (var volumeData in wrapper.volumeDataList)
        {
            SetVolume(volumeData.audioGroup, volumeData.volume);
        }
    }

    public float GetVolume(AudioGroup name)
    {
        float ret = 0;
        audioMixer.GetFloat(name.ToString(), out ret);
        return ret;
    }

    public void SetVolume(AudioGroup name, float value)
    {
        audioMixer.SetFloat(name.ToString(), value);
    }
}

//AudioMixerの値を取得する用の列挙型
//※AudioMixer側の文字列と一致させておくこと
public enum AudioGroup
{
    Master,
    BGM,
    SE
}

[Serializable]
public class VolumeData
{
    public AudioGroup audioGroup;
    public float volume;
    public VolumeData(AudioGroup audioGroup, float volume)
    {
        this.audioGroup = audioGroup;
        this.volume = volume;
    }
}

[Serializable]
public class VolumeDataListWrapper
{
    public List<VolumeData> volumeDataList;
    public VolumeDataListWrapper()
    {
        volumeDataList = new List<VolumeData>();
    }
}