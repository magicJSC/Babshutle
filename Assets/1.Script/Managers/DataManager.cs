using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    #region ¿Ã∫•∆Æ 1  
    public bool Get_Key;
    public bool Get_Material;
    public bool Talk_Jam;
    public bool Clear_Jam;
    #endregion


    public void Init()
    {
        Get_Key = false;
        Get_Material = false;
        Talk_Jam = false;
        Clear_Jam = false;
    }
}
