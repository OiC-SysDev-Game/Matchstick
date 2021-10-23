using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private AudioGroupName groupName;
    [SerializeField] private Slider slider;
    [SerializeField] private float volumeMin;
    [SerializeField] private float volumeMax;
    void Start()
    {
        slider.minValue = volumeMin;
        slider.maxValue = volumeMax;
        slider.value = audioManager.GetVolume(groupName);
        slider.onValueChanged.AddListener(delegate { ValueChanged(); });
    }

    void ValueChanged()
    {
        audioManager.SetVolume(groupName, slider.value);
    }
}
