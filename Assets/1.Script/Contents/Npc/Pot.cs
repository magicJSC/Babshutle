using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : Npc
{
    protected override void ChoiceTalk()
    {
        if (Managers.Data.Get_Gold)
            return;

        if (!Managers.Data.Use_Bujeok)
            Instantiate(Resources.Load<GameObject>("Talk/Talk_Pot1"));
        else if (Managers.Data.Use_Bujeok && !Managers.Data.Get_Material)
            Instantiate(Resources.Load<GameObject>("Talk/Talk_Pot2"));
        else if (Managers.Data.Use_Bujeok && Managers.Data.Get_Material)
            Instantiate(Resources.Load<GameObject>("Talk/Talk_Pot3"));
    }
}
