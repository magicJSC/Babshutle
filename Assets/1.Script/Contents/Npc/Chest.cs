using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Npc
{
    protected override void ChoiceTalk()
    {
        if (!Managers.Data.Get_Key)
            Instantiate(Resources.Load<GameObject>("Talk/Chest_Lock"));
        else if (Managers.Data.Get_Key)
        {
            Instantiate(Resources.Load<GameObject>("Talk/Chest_Open"));
            gameObject.layer = 6;
        }
    }
}
