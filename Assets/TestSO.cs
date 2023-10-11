using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSO : MonoBehaviour
{
    [SerializeField] SO_Unit unit;
    [SerializeField] SO_CardBase Card;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(unit.UintID);
        Debug.Log(Card.CardId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
