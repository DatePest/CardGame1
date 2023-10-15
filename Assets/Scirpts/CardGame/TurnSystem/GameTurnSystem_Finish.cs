using System.Collections;
using UnityEngine;


public class GameTurnSystem_Finish : I_StatePattern
{
    protected CardGameManager gameManagerInstance;
    public GameTurnSystem_Finish(CardGameManager GameManagerInstance)
    {
        gameManagerInstance = GameManagerInstance;
    }
    public override void StateEnter()
    {
        //throw new System.NotImplementedException();
    }

    public override void StateExit()
    {
        //throw new System.NotImplementedException();
    }

    public override void StateUpdate()
    {
        //throw new System.NotImplementedException();
    }
}
