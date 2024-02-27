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
            GetComponent<Chest>().ChoiceTalk();
        else if (id == 2)
            GetComponent<StrangeSeed>().ChoiceTalk();
        else if(id == 3)
            GetComponent<Pillar>().ChoiceTalk();
        else if(id == 4)
            GetComponent<Pot>().ChoiceTalk();
        else if(id == 5)
            GetComponent<Pikachu>().ChoiceTalk();
    }

    protected virtual void ChoiceTalk()
    {

    }
}
