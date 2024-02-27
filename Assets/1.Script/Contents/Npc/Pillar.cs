using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : Npc
{
    protected override void ChoiceTalk()
    {
        if(Managers.Data.Use_Bujeok)
        {
            Instantiate(Resources.Load<GameObject>("Talk/Talk_Pillar3"));
            return;
        }

        if (!Managers.Data.Get_Bujeok)
            Instantiate(Resources.Load<GameObject>("Talk/Talk_Pillar1"));
        else if (Managers.Data.Get_Bujeok)
            Instantiate(Resources.Load<GameObject>("Talk/Talk_Pillar2"));
    }
}
