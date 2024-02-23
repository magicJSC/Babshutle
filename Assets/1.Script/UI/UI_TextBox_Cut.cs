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
    float delay;    //보통 : 0.05

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
        int index = 0;  //티이핑에 필요한 index
        delay = 0.05f;  //보통 딜레이 시간
        speak.text = ""; //대화창 대화 초기화
        noTyping = false;
        while (index < talkData.Length)
        {
            string s = talkData.Substring(index, 1);
            index++;
            if (s == "%")   //전체띄우기(문장 앞에 띄울것)
            {
                noTyping = true;
            }
            else if (s == "/")  //끝에 있을때 대화가 끝난다
            {
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

                if (!noTyping)
                    delay = 0.05f;
            }
            else if (s == "^")   // ^다음에 나온 수에 따라 딜레이를 준다
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
