using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioGroup groupName;
    [SerializeField] private Slider slider;
    [SerializeField] private float volumeMin;
    [SerializeField] private float volumeMax;

    void Start()
    {
        slider.minValue = volumeMin;
        slider.maxValue = volumeMax;
        slider.SetValueWithoutNotify(AudioManager.Instance.GetVolume(groupName));
        slider.onValueChanged.AddListener(delegate { ValueChanged(); });
    }

    void ValueChanged()
    {
        if (slider.value == slider.minValue)
        {
            AudioManager.Instance.SetVolume(groupName, -80.0f);
        }
        else
        {
            AudioManager.Instance.SetVolume(groupName, slider.value);
        }
    }
}
