using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class UI_TalkTextBox : UI_Base
{
    //���࿡ �̺�Ʈ�� ������ [SerializeField]�� �Լ� �̸��� �ް� eventManager���� ���� �ִ� �Լ��� Invoke�� ���� �����ش�
    [SerializeField] string evtName;
    [SerializeField] GameObject evtObj;

    [SerializeField] public bool isChoiceText;

    [Header("Texts")]
    TextMeshProUGUI speak;
    TextMeshProUGUI name;
    Image cursor;

    bool noTyping = false;
    float delay;    //���� : 0.05
    

    TalkManager t_m;
    public override void Init()
    {

    }

    enum TextBox
    {
        Speak,
        Name
    }
    enum image 
    { 
        Cursor,
    }


    void Start()
    {
        Bind<TextMeshProUGUI>(typeof(TextBox));
        Bind<Image>(typeof(image));
        speak = GetText((int)TextBox.Speak);
        name = GetText((int)TextBox.Name);
        cursor = GetImage((int)image.Cursor);
        if(cursor != null)
         cursor.gameObject.SetActive(false);
        Managers.Game.canTalk = false;  //�ؽ�Ʈ�� ���ö� ��ȭ�� �� ����
        t_m = transform.parent.GetComponent<TalkManager>();
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        string talkData = speak.text;
        int index = 0;  //Ƽ���ο� �ʿ��� index
        delay = 0.05f;  //���� ������ �ð�
        speak.text = ""; //��ȭâ ��ȭ �ʱ�ȭ
        Managers.Game.isTalking = true; //��ȭ�� �ϰ� ������ �ٸ� ��ȭ�� �Ұ���
        t_m.endTyping = false;
        noTyping = false;
        while (index < talkData.Length)
        {
            string s = talkData.Substring(index, 1);
            index++;
            if (s == "%")   //��ü����(���� �տ� ����)
            {
                noTyping = true;
            }
            else if (s == "/")  //���� ������ ��ȭ�� ������
            {
                Managers.Game.isTalking = false;
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
                if (!noTyping)
                    delay = 0;
                 yield return new WaitForSeconds(delay / 5);
            }
            else
                speak.text += s;
            if (Managers.Game.GameOver)
                yield break;
            GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Typing")) ;
            if (!noTyping)
                yield return new WaitForSeconds(delay);
        }
        if (cursor != null)
            cursor.gameObject.SetActive(true);
        if (evtName != "")
        {
            if (evtObj == null)
                evtObj = gameObject;
            Managers.instance.SetEvent(evtName,evtObj);
        }

        if(!isChoiceText)
         t_m.endTyping = true;
    }
}
