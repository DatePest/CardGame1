using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton_T_Mono<AudioManager>
{
    [SerializeField] AudioMixer audioMixer;
    //AudioMixerGroup[] audioMixerGroups = new AudioMixerGroup[4];
    public string[] Names { get; private set; } = new string[4];
    public Dictionary<string, AudioMixerGroup> _OutaudioMixer { get; private set; } = new();
    public Dictionary<string, float> Volumes { get; private set; } = new();
    private void Awake()
    {
        base.Awake();
        Set();
        for (int i = 0; i < Names.Length; i++)
        {
            _OutaudioMixer.Add(Names[i], audioMixer.FindMatchingGroups(Names[i])[0]);

            Volumes.Add(Names[i], GetVolume(Names[i]));
        }
        DontDestroyOnLoad(gameObject);
    }
    void Set()
    {
        var Array = Enum.GetValues(typeof(AudioMixerG)) as AudioMixerG[];
        int i = 0;
        foreach(var a in Array)
        {
            Names[i] = a.ToString();
            i++;
        }
    }
    public float GetVolume(string s)
    {
        audioMixer.GetFloat(s, out var F);
        return F;
    }
    public void SetVolume(string s, float f)
    {
        audioMixer.SetFloat(s, Mathf.Lerp(-80, 20, f));
        Volumes[s]= GetVolume(s);
    }
    public void SetMute(string s,bool b)
    {
        if(!b)
        audioMixer.SetFloat(s, -80f);
        else
            audioMixer.SetFloat(s, Volumes[s]);
    }
}

public enum AudioMixerG
{
    Master,
    Bgm,
    EFF,
    Voice

}

