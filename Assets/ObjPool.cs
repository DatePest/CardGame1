using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    [SerializeField] GameObject Traget;
    [SerializeField] int Setint;
    public List<GameObject> EnableList { get; private set; } = new();
    public List<GameObject> DisenableList { get; private set; } = new();
    private void Awake()
    {
        Set(Setint);
    }
    public void Set(int i)
    {
        for (int a = 0; a < i; a++)
        {
            GameObject g = Instantiate(Traget, transform);
            DisenableList.Add(g);
            g.SetActive(false);
        }
    }
    public void GetPool(out GameObject g)
    {
        if(DisenableList.Count > 0)
        {
            g = DisenableList[0];
            DisenableList.Remove(g);
            g.SetActive(true);
        }
        else
            g = Instantiate(Traget, transform);
        EnableList.Add(g);
        
    }
    public void  RemovePool(GameObject g)
    {
        EnableList.Remove(g);
        DisenableList.Add(g);
        g.SetActive(false);
    }
    public void RemoveAll()
    {
        int Count = EnableList.Count;
        for (int i =0; i< Count; i++)
        {
            RemovePool(EnableList[0]);
        }
    }

}
