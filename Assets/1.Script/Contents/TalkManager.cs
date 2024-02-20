using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    int talkIndex;
    [HideInInspector]
    public bool endTyping;
    void Start()
    {
        for(int i = 0;i<transform.childCount;i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
        talkIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Managers.Game.isTalking && endTyping)
            {
                transform.GetChild(talkIndex).gameObject.SetActive(false);
                transform.GetChild(++talkIndex).gameObject.SetActive(true);
            }
            else if (!Managers.Game.isTalking)  //대화 전체가 끝났을때
            {
                Managers.Game.canTalk = true;
                Destroy(gameObject);
            }
        }
    }
}
