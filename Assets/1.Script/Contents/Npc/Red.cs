using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : Npc
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void ChoiceTalk()
    {
        if (Managers.Data.Get_Red)
            return;

        if (!Managers.Data.Get_White) //동료를 구하지 못하고 Red와 대화
        {
            Instantiate(Resources.Load<GameObject>("Talk/Evt1_Red_1"));
            Managers.Data.Talk_Red_1 = true;
        }
        else if (Managers.Data.Get_White && Managers.Data.Talk_Red_1)  //Red와 대화 후 동료를 구한
        {
            Instantiate(Resources.Load<GameObject>("Talk/Evt1_Red_2"));
            Managers.Data.Get_Red = true;
        }
        else if (Managers.Data.Get_White && !Managers.Data.Talk_Red_1)  //Red와 대화 후 동료를 구한
        {
            Instantiate(Resources.Load<GameObject>("Talk/Evt1_Red_3"));
            Managers.Data.Get_Red = true;
        }
    }
}
