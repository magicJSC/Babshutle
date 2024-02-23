using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeSeed : Npc
{
    [SerializeField] GameObject jam;
    protected override void ChoiceTalk()
    {
        if (Managers.Data.Clear_Jam)
            return;

        if (!Managers.Data.Talk_Jam)
            Instantiate(Resources.Load<GameObject>("Talk/Seed_1"));
        else if (Managers.Data.Talk_Jam)
        {
            Instantiate(Resources.Load<GameObject>("Talk/Seed_2"));
            Managers.Data.Clear_Jam = true;
        }
    }
}
