using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class UI_TextBox_Cut : UI_Base
{
    [Header("Texts")]
    TextMeshProUGUI speak;
    TextMeshProUGUI name;
    Image cursor;

    bool noTyping = false;
    float delay;    //���� : 0.05

    bool nextSpeak;
    double initTime;
    PlayableDirector cut;
    public override void Init()
    {

    }

    enum TextBox
    {
        Speak
    }
    enum image
    {
        Cursor
    }

    void Start()
    {
        Bind<TextMeshProUGUI>(typeof(TextBox));
        Bind<Image>(typeof(image));
        speak = GetText((int)TextBox.Speak);
        cursor = GetImage((int)image.Cursor);
        cursor.gameObject.SetActive(false);
        cut =transform.parent.GetComponent<PlayableDirector>();
        nextSpeak = false;
        StartCoroutine(Typing());
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Mouse0)) && nextSpeak)
        {
            cut.Play();
        }
    }

    IEnumerator Typing()
    {
        string talkData = speak.text;
        int index = 0;  //Ƽ���ο� �ʿ��� index
        delay = 0.05f;  //���� ������ �ð�
        speak.text = ""; //��ȭâ ��ȭ �ʱ�ȭ
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

                if (!noTyping)
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
            if (!noTyping)
                yield return new WaitForSeconds(delay);
        }
        nextSpeak = true;
        initTime = cut.time;
        cut.Stop();
        cut.initialTime = initTime;
        cursor.gameObject.SetActive(true);
    }
}
