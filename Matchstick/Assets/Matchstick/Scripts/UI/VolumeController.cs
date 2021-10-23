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
        slider.value = AudioManager.Instance.GetVolume(groupName);
        slider.onValueChanged.AddListener(delegate { ValueChanged(); });
    }

    void ValueChanged()
    {
        AudioManager.Instance.SetVolume(groupName, slider.value);
    }
}
