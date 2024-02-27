using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikachu : Npc
{
    protected override void ChoiceTalk()
    {
        if (Managers.Data.Tutorial)
          Instantiate(Resources.Load<GameObject>("Talk/Talk_Pik1"));
        else if (Managers.Data.Get_Gold)
            Instantiate(Resources.Load<GameObject>("Cut/Happy"));
        else
            Instantiate(Resources.Load<GameObject>("Talk/Talk_Pik2"));
    }
}
