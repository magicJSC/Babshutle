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
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Managers.Game.isTalking && endTyping && !Managers.Game.GameOver)
            {
                GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Click"));
                transform.GetChild(talkIndex).gameObject.SetActive(false);
                transform.GetChild(++talkIndex).gameObject.SetActive(true);
            }
            else if (!Managers.Game.isTalking)  //대화 전체가 끝났을때
            {
                if (transform.GetChild(talkIndex).GetComponent<UI_TalkTextBox>().isChoiceText)
                    return;

                Managers.Game.canTalk = true;
                Destroy(gameObject);
            }
        }
    }
}
