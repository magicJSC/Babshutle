using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Choice1 : UI_Base
{
    Image c1;
    Image c2;

    public GameObject talk;
    public override void Init()
    {
        
    }

    enum image
    {
        Choice1,
        Choice2
    }

    private void Start()
    {
        Bind<Image>(typeof(image));
        c1 = Get<Image>((int)image.Choice1);
        c2 = Get<Image>((int)image.Choice2);

        UI_EventHandler e = c1.GetComponent<UI_EventHandler>();
        e.OnClick += (PointerEventData evt) => { Instantiate(Resources.Load<GameObject>("Talk/Choice1")); c1.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Click")); Destroy(talk); Destroy(gameObject); };   //설명 : 전체 대화 -> 타임라인 이벤트
        e.OnExit += (PointerEventData evt) => { c1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white; };
        e.OnEnter += (PointerEventData evt) => { c1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.yellow; c1.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Choice")); };

        e = c2.GetComponent<UI_EventHandler>();
        e.OnClick += (PointerEventData evt) => { Destroy(gameObject); };
        e.OnExit += (PointerEventData evt) => { c2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white; };
        e.OnEnter += (PointerEventData evt) => { c2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.yellow; c1.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Choice")); };
    }
}
