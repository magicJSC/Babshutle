using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class UI_TalkTextBox : UI_Base
{
    //만약에 이벤트가 있으면 [SerializeField]로 함수 이름을 받고 eventManager같은 곳에 있는 함수를 Invoke로 실행 시켜준다
    [SerializeField] string evtName;
    [SerializeField] GameObject evtObj;

    [SerializeField] public bool isChoiceText;

    [Header("Texts")]
    TextMeshProUGUI speak;
    TextMeshProUGUI name;
    Image cursor;

    bool noTyping = false;
    float delay;    //보통 : 0.05
    

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
        Managers.Game.canTalk = false;  //텍스트가 나올땐 대화할 수 없다
        t_m = transform.parent.GetComponent<TalkManager>();
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        string talkData = speak.text;
        int index = 0;  //티이핑에 필요한 index
        delay = 0.05f;  //보통 딜레이 시간
        speak.text = ""; //대화창 대화 초기화
        Managers.Game.isTalking = true; //대화를 하고 있을땐 다른 대화는 불가능
        t_m.endTyping = false;
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
                Managers.Game.isTalking = false;
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
