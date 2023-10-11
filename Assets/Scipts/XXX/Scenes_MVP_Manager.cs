using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenes_MVP_Manager : Singleton_T_Mono<Scenes_MVP_Manager>
{
    public Scenes_View View;
    public Scenes_Presenter Presenter;
    //public Scenes_Model Model;

    // Start is called before the first frame update
    void Start()
    {
        View = FindObjectOfType<Scenes_View>();
        Presenter = FindObjectOfType<Scenes_Presenter>();
        //Model = FindObjectOfType<Scenes_Model>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
