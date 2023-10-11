using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Setting_Screen : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropdown, HzDropdown;
    [SerializeField] Toggle _IsFullToggle;
     List<Resolution> _resolutions = new();
    [SerializeField] bool IsFull =false;
    [SerializeField] int[] HzS = new int[4] { 30,60,75,120} ;
    private void Awake()
    {
        _IsFullToggle.onValueChanged.AddListener(SetFull);
        SetResolutionDropdown(resolutionDropdown);
        SetHzDropdown(HzDropdown);
    }
    void SetResolutionDropdown(TMP_Dropdown _Dropdown)
    {
        _resolutions.Clear();
            var T_resolutions = Screen.resolutions;
        _Dropdown.ClearOptions();
        List<string> ops = new();
        int Tamp = 0;
        for (int i = 0; i < T_resolutions.Length; i++)
        {
            //string s = _resolutions[i].width + "X" + _resolutions[i].height + " " + _resolutions[i].refreshRateRatio + "Hz";
            string s = T_resolutions[i].width + "X" + T_resolutions[i].height;
            if (ops.Contains(s)) continue;
            ops.Add(s);
            _resolutions.Add(T_resolutions[i]);
            if (T_resolutions[i].width == Screen.currentResolution.width && T_resolutions[i].height==Screen.currentResolution.height)
            {
                Tamp = i;
            }
        }
        _Dropdown.AddOptions(ops);
        _Dropdown.value = Tamp;
        _Dropdown.onValueChanged.AddListener(SetResolution);
    }
    void SetHzDropdown(TMP_Dropdown _Dropdown)
    {
        HzDropdown.ClearOptions();
        List<string> ops = new();
        foreach(var a in HzS)
        {
            ops.Add(a.ToString());
        }
        _Dropdown.AddOptions(ops);
        _Dropdown.value = 1;
        _Dropdown.onValueChanged.AddListener(SetHz);
    }
    private void SetFull(bool B)
    {
        IsFull = B;
        Screen.fullScreen = IsFull;
    }

    private void SetResolution(int i)
    {
        //Debug.Log(_resolutions.Count +"   I=" + i); ;
        Screen.SetResolution(_resolutions[i].width, _resolutions[i].height, IsFull);
    }

    private void SetHz(int i)
    {
        Application.targetFrameRate = HzS[i];
    }
}
