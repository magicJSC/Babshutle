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

        if (!Managers.Data.Get_Box) //���ڸ� ��� ��
        {
            Instantiate(Resources.Load<GameObject>("Talk/Evt1_White_1"));
            Managers.Data.Talk_White_1 = true;
        }
        else if (Managers.Data.Get_Box && !Managers.Data.Talk_White_1) //���ڸ� ���� �� ��ȭ,White ��ȭ x
        { 
            Instantiate(Resources.Load<GameObject>("Talk/Evt1_White_2"));
            Managers.Data.Get_White = true;
        }
        else if (Managers.Data.Get_Box && Managers.Data.Talk_White_1) //���ڸ� ���� ��,WHite ��ȭ o
        {
            Instantiate(Resources.Load<GameObject>("Talk/Evt1_White_3"));
            Managers.Data.Get_White = true;
        }

    }
}
