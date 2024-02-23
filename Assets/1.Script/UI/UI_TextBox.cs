using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_TextBox : UI_Base
{
    [Header("Texts")]
    TextMeshProUGUI speak;
    TextMeshProUGUI name;
    Image character;
    Image cursor;

    bool noTyping;
    float delay;    //보통 : 0.05
    bool endTyping;

    int talkIndex;
    [HideInInspector]
    public InterObj inter;
    public override void Init()
    {

    }

    enum Text
    {
        Speak,
        Name
    }
    enum image 
    { 
        Char,
        Cursor
    }


    void Start()
    {
        Bind<TextMeshProUGUI>(typeof(Text));
        Bind<Image>(typeof(image));
        speak = Get<TextMeshProUGUI>((int)Text.Speak);
        name = Get<TextMeshProUGUI>((int)Text.Name);
        cursor = Get<Image>((int)image.Cursor);
        cursor.gameObject.SetActive(false);
        character = Get<Image>((int)image.Char);

        Managers.Game.canTalk = false;  //텍스트가 나올땐 대화할 수 없다
        talkIndex = inter.repeatTalk ? inter.repeatIndex : 0;
        StartCoroutine(Typing());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Managers.Game.isTalking && endTyping)
            {
                talkIndex++;
                StartCoroutine(Typing());
            }
            else if (!Managers.Game.isTalking)
            {
                Managers.Game.canTalk = true;
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Typing()
    {
        string talkData = inter.talks[talkIndex].speak;
        name.text = inter.talks[talkIndex].name;
        if (inter.talks[talkIndex].character != null)
        {
            character.gameObject.SetActive(true);
            character.sprite = inter.talks[talkIndex].character; 
        }
        else
            character.gameObject.SetActive(false);
        int index = 0;  //티이핑에 필요한 index
        delay = 0.05f;  //보통 딜레이 시간
        speak.text = ""; //대화창 대화 초기화
        Managers.Game.isTalking = true; //대화를 하고 있을땐 다른 대화는 불가능
        endTyping = false;
        noTyping = false;
        while (index < talkData.Length)
        {
            string s = talkData.Substring(index, 1);
            index++;
            if (s == "%")   //전체띄우기(문장 앞에 띄울것)
            {
                noTyping= true;
            }
            else if (s == "/")  //끝에 있을때 대화가 끝난다
            {
                Managers.Game.isTalking = false;
                inter.repeatTalk = true;
                break;
            }
            else if (s == "<")  //색깔이나 폰트나 글자 크기를 바꿀때 쓰는 꺽쇠
            {
                speak.text += s;
                delay = 0;
            }
            else if (s == ">")
            {
                speak.text += s;

                if(!noTyping)
                  delay = 0.05f;
            }
            else if (s == "^")   // ^다음에 나온 수에 따라 딜레이를 준다
            {
                s = talkData.Substring(index, 1);
                index++;
                int delay = int.Parse(s);
                yield return new WaitForSeconds(delay / 5);
            }
            else
                speak.text += s;
            
            if(!noTyping)
                yield return new WaitForSeconds(delay);
        }
        cursor.gameObject.SetActive(true);
        if (inter.talks[talkIndex].eventName != "")
        {
            if (inter.talks[talkIndex].eventObj == null)
                inter.talks[talkIndex].eventObj = gameObject;
            Managers.instance.SetEvent(inter.talks[talkIndex].eventName, inter.talks[talkIndex].eventObj);
        }
        endTyping = true;
       
    }
}
