using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01 : MonoBehaviour
{
    [SerializeField] GameObject GG;
    List<GameObject> gameObjects =new();
    void Start()
    {
        Set(6);
    }
    public void Set(int i)
    {
        for (int a = 0; a < i; a++)
        {
            GameObject g = Instantiate(GG, transform);
            gameObjects.Add(g);
            g.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach(var a in gameObjects)
            {
                a.SetActive(false);
            }

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (var a in gameObjects)
            {
                a.SetActive(true);
            }
        }
    }
}
