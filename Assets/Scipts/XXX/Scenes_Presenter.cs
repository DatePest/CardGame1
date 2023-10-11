using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenes_Presenter : MonoBehaviour
{
    [SerializeField] CardSpawnScript cardSpawnScript;
    // Start is called before the first frame update
    void Start()
    {
        cardSpawnScript = FindObjectOfType<CardSpawnScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //Shuffl(Deck);
            //cardSpawnScript.Shuffle_CardsServerRpc(ref cardSpawnScript.deck);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //Shuffl(Deck);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //cardSpawnScript.SpawnDeckToHand();
        }
    }
}
