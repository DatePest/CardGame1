using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class CardCol : MonoBehaviour
{
    [SerializeField] List<SO_CardBase> Cards, Deck, DisDeck = new();
    //[SerializeField] List<GameObject> Deck = new();
    [SerializeField] GameObject target,card;
    bool _isGoEndRunning;
    void Start()
    {
        //for (int i = 0; i < 40; i++)
        //{
        //    Deck.Add(i);
        //}

        //GOstart(5);
    }

    
    public void Shuffl<T>( IList<T> values)
    {
        Shuffle.Shuffle_List(values);
    }

    public void GOstart(int cont)
    {
        for(int i=0; i < cont; i++)
        {
            if (Deck.Count == 0)
                break;
            //Cards.Add(Instantiate(card, target.transform));
            var a = Deck[0];
            Deck.Remove(a);
            Cards.Add(a);

            
        }
        
    }

    public async void GOend()
    {if (_isGoEndRunning)
            return;

        _isGoEndRunning = true;

        ToDiskDeck(Cards.Count);
        await Task.Delay(1000);
        Debug.Log("Sleepend");


        GOstart(5);
        _isGoEndRunning = false;
    }

    public void ToDiskDeck(int cont)
    {
        //List<int> tamp = new();
        for (int i = 0; i < cont; i++)
        {
            var c = Cards[Random.Range(0, Cards.Count)];
            DisDeck.Add(c);
            Cards.Remove(c);
        }
        
       
    }
}
