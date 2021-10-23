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

//AudioMixer�̒l���擾����p�̗񋓌^
//��AudioMixer���̕�����ƈ�v�����Ă�������
public enum AudioGroupName
{
    Master,
    BGM,
    SE
}
