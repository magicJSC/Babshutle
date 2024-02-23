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
    float delay;    //���� : 0.05
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

        Managers.Game.canTalk = false;  //�ؽ�Ʈ�� ���ö� ��ȭ�� �� ����
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
        int index = 0;  //Ƽ���ο� �ʿ��� index
        delay = 0.05f;  //���� ������ �ð�
        speak.text = ""; //��ȭâ ��ȭ �ʱ�ȭ
        Managers.Game.isTalking = true; //��ȭ�� �ϰ� ������ �ٸ� ��ȭ�� �Ұ���
        endTyping = false;
        noTyping = false;
        while (index < talkData.Length)
        {
            string s = talkData.Substring(index, 1);
            index++;
            if (s == "%")   //��ü����(���� �տ� ����)
            {
                noTyping= true;
            }
            else if (s == "/")  //���� ������ ��ȭ�� ������
            {
                Managers.Game.isTalking = false;
                inter.repeatTalk = true;
                break;
            }
            else if (s == "<")  //�����̳� ��Ʈ�� ���� ũ�⸦ �ٲܶ� ���� ����
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
            else if (s == "^")   // ^������ ���� ���� ���� �����̸� �ش�
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
