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
                    Debug.LogError(typeof(AudioManager) + "���V�[���ɑ��݂��܂���");
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
        //���ʂ̃f�[�^�̓ǂݍ���
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
    /// ���݂̉��ʂ̃f�[�^���t�@�C���ɕۑ�����֐�
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
    /// ���ʂ̃f�[�^���t�@�C������ǂݎ��K�p����֐�
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

//AudioMixer�̒l���擾����p�̗񋓌^
//��AudioMixer���̕�����ƈ�v�����Ă�������
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