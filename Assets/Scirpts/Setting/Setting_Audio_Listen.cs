using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Setting_Audio_Listen : MonoBehaviour
{
    Slider slider;
    Toggle _toggle;
    [SerializeField] AudioMixerG TargetOutAudioMixerG;
    void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        slider.onValueChanged.AddListener(_event);
        slider.value =Mathf.InverseLerp(-80,20, AudioManager.Instance.GetVolume(TargetOutAudioMixerG.ToString()));
        _toggle = GetComponentInChildren<Toggle>();
        _toggle.onValueChanged.AddListener(_event2);
    }

    private void _event2(bool B)
    {
        AudioManager.Instance.SetMute(TargetOutAudioMixerG.ToString(),B);
    }

    private void _event(float f)
    {
        AudioManager.Instance.SetVolume(TargetOutAudioMixerG.ToString(), f);
    }
}
