using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Swich : MonoBehaviour
{
    [SerializeField] GameObject[] Gs;

    public void AllDis()
    {
        foreach(var a in Gs)
        {
            a.SetActive(false);
        }
    }
}
