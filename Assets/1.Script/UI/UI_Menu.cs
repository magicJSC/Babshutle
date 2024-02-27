using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Menu : UI_Base
{
    Image c1;
    Image c2;

    [HideInInspector]
    public GameObject menuManager;
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
        e.OnClick += (PointerEventData evt) =>
        {
            menuManager.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Click"));
            menuManager.GetComponent<MenuManager>().showingMenu = false;
            Managers.Game.GameOver = false;
            Time.timeScale = 1;
            Destroy(gameObject);
        };   
        e.OnExit += (PointerEventData evt) => { c1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white; };
        e.OnEnter += (PointerEventData evt) => { c1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.yellow; c1.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Choice")); };

        e = c2.GetComponent<UI_EventHandler>();
        e.OnClick += (PointerEventData evt) => { SceneManager.LoadScene("Main"); };
        e.OnExit += (PointerEventData evt) => { c2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white; };
        e.OnEnter += (PointerEventData evt) => { c2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.yellow; c1.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/Choice")); };
    }
}
