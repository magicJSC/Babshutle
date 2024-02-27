using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Main : UI_Base
{
    public override void Init()
    {
        
    }

    public Image c1;
    public Image c2;
    public Image c3;

    
    private void Start()
    {
        UI_EventHandler e = c1.GetComponent<UI_EventHandler>();
        e.OnClick += (PointerEventData evt) => { Managers.Data.Tutorial = true; Managers.Data.Init(); GetComponent<Animator>().Play("ChangeScene"); c1.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Click")); };   //설명 : 전체 대화 -> 타임라인 이벤트
        e.OnExit += (PointerEventData evt) => { c1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white; };
        e.OnEnter += (PointerEventData evt) => { c1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.yellow; c1.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Choice")); };

        e = c3.GetComponent<UI_EventHandler>();
        e.OnClick += (PointerEventData evt) => { Application.Quit(); };
        e.OnExit += (PointerEventData evt) => { c3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white; };
        e.OnEnter += (PointerEventData evt) => { c3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.yellow; c3.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Choice")); };

        e = c2.GetComponent<UI_EventHandler>();
        e.OnClick += (PointerEventData evt) => { Managers.Data.Tutorial = false; Managers.Data.Init(); GetComponent<Animator>().Play("ChangeScene"); c1.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Click")); };
        e.OnExit += (PointerEventData evt) => { c2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white; };
        e.OnEnter += (PointerEventData evt) => { c2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.yellow; c2.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Choice")); };
    }


    void Change()
    {
        SceneManager.LoadScene("Game");
    }
}
