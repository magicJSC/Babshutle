using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
   
    void Start()
    {
        if(Managers.Data.Tutorial)
            transform.GetChild(0).gameObject.SetActive(true);
    }
}
