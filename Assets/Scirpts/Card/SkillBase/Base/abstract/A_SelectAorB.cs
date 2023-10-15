using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class  A_SelectAorB 
{
    int T;
    public async Task<AbilityNeedData> UserSelectAorB(AbilityNeedData data)
    {
        T = 0;
        data.CurrentUsePlayer.PlayerTrigger.SetActiveEX_Select(CardCheckTimesADD,true,true);
        data.CurrentUsePlayer.PlayerTrigger.SetActiveEX_Select_SetText("A", "B");
        while (T == 0)
        {
            await Task.Delay(200);
        }
        if (T == 1)
            data.AbilityValue4 = 1;
        else
            data.AbilityValue4 = 2;
        T = 0;
        return data;
    }

    void CardCheckTimesADD(int i)
    {
        T = i;
    }
}
