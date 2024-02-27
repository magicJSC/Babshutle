using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_Choice2 : UI_Base
{
    Image c1;
    Image c2;
    Image c3;

    public GameObject talk;
    public override void Init()
    {

    }

    enum image
    {
        Choice1,
        Choice2,
        Choice3
    }

    private void Start()
    {
        Bind<Image>(typeof(image));
        c1 = Get<Image>((int)image.Choice1);
        c2 = Get<Image>((int)image.Choice2);
        c3 = Get<Image>((int)image.Choice3);

        UI_EventHandler e = c1.GetComponent<UI_EventHandler>();
        e.OnClick += (PointerEventData evt) => {
            Destroy(talk);
            Destroy(gameObject);
            Managers.Data.Get_Gold = true; Instantiate(Resources.Load<GameObject>("Talk/Get_Gold")); Managers.Game.canTalk = true; };
        e.OnEnter += (PointerEventData evt) => { c1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.yellow; c1.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Choice")); };

        e = c2.GetComponent<UI_EventHandler>();
        e.OnClick += (PointerEventData evt) => { Instantiate(Resources.Load<GameObject>("Cut/SpecialEnd")); Managers.Game.canTalk = true; Destroy(talk);  Destroy(gameObject); };
        e.OnExit += (PointerEventData evt) => { c2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white; };
        e.OnEnter += (PointerEventData evt) => { c2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.yellow; c1.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Choice")); };

        e = c3.GetComponent<UI_EventHandler>();
        e.OnClick += (PointerEventData evt) => { Managers.Game.canTalk = true; Destroy(talk); Destroy(gameObject); };
        e.OnExit += (PointerEventData evt) => { c3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white; };
        e.OnEnter += (PointerEventData evt) => { c3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.yellow; c1.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Choice")); };
    }
}
