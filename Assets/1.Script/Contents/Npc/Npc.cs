using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] int id;
    protected virtual void Start()
    {
       
    }

    public void CheckObj()
    {
        if (id == 1)
            GetComponent<Red>().ChoiceTalk();
        else if (id == 2)
            GetComponent<White>().ChoiceTalk();
    }

    protected virtual void ChoiceTalk()
    {

    }
}
