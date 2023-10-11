using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestIs : MonoBehaviour
{
    [SerializeField] SO_SKillAbility Test;
    // Start is called before the first frame update

    public event Func<int, int> Test1;
    // Update is called once per frame


    private void Start()
    {
        Test1 += Fun01;
    }

    void Testfun()
    {
        var delegates = Test1.GetInvocationList();
        //int result = value;
    }

    private int Fun01(int arg)
    {
        return arg+= arg;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Test is Skill_TS)
            {
                Debug.Log("Turn");
            }
            else
                Debug.Log("False");
        }
    }
}
