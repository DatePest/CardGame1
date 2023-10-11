using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] AbilityBase Ability;
    [SerializeField] BuffBase buff;
    [SerializeField] SO_Unit target;
    [SerializeField] MouseManager mouse;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AbilityDataWait());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            target.AddAbility(Instantiate(Ability));
            Debug.Log("AddAbility");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            target.AddBuff(Instantiate(buff));
            //Debug.Log("BuffADD");
        }

    }


    private IEnumerator AbilityDataWait()
    {
        while (target == null)
        {
            try
            {
                var ToTarget = mouse.CurrnetSelect.GetComponent<MapSolt>();
                //target=ToTarget.unitSolt.Unit;
            }
            catch
            {
            }
            

            yield return new WaitForSeconds(0.2F);
        }
    }
}
