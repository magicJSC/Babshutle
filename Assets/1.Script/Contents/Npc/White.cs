using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class White : Npc
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void ChoiceTalk()
    {
        if (Managers.Data.Get_White)
            return;

        if (!Managers.Data.Get_Box) //상자를 얻기 전
        {
            Instantiate(Resources.Load<GameObject>("Talk/Evt1_White_1"));
            Managers.Data.Talk_White_1 = true;
        }
        else if (Managers.Data.Get_Box && !Managers.Data.Talk_White_1) //상자를 얻은 후 대화,White 대화 x
        { 
            Instantiate(Resources.Load<GameObject>("Talk/Evt1_White_2"));
            Managers.Data.Get_White = true;
        }
        else if (Managers.Data.Get_Box && Managers.Data.Talk_White_1) //상자를 얻은 후,WHite 대화 o
        {
            Instantiate(Resources.Load<GameObject>("Talk/Evt1_White_3"));
            Managers.Data.Get_White = true;
        }

    }
}
