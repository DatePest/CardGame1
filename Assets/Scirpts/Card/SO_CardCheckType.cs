using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SO_CardCheckType : ScriptableObject
{
    [SerializeField] protected ReTurnFox F; 
    public abstract List<GameObject> SearchCheckCard(List<GameObject> GList);
    public abstract bool CheckCard(GameObject G);
  
}
