using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers instance { get { if (s_instance == null) { Init(); } return s_instance; } }

    GameManager game = new GameManager();
    public static GameManager Game { get { return s_instance.game; } }

    DataManager data = new DataManager();
    public static DataManager Data { get { return s_instance.data; } } 
    private void Awake()
    {
        Init();
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Manager");
            if(go == null)
            {
                go = new GameObject { name = "@Manager" };
                go.AddComponent<Managers>();
            }

            s_instance = go.GetComponent<Managers>();
            DontDestroyOnLoad(go);
        }
    }

    GameObject InteractObj;
    public void SetEvent(string eventName, GameObject go = null)
    {
        if (go != null)  //상호작용한 오브젝트를 이용한 이벤트면 인수에 넣어준다
        {
            InteractObj = go;
        }
        StartCoroutine(eventName);
    }

    #region 이벤트 함수
    IEnumerator Choice1()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("UI/UI_Choice1"));
        go.GetComponent<UI_Choice1>().talk = InteractObj;
        yield return null;
    }


    IEnumerator Explain()
    {
        instance.data.Tutorial = false;
        instance.game.canTalk = true;
        Destroy(InteractObj);
        Instantiate(Resources.Load<GameObject>("Cut/Explain"));
        yield return null;
    }

    IEnumerator Cook()
    {
        instance.game.gu.GetComponent<GUController>().agent.speed = 1;
        yield return null;
    }

    IEnumerator Get_Key()
    {
        instance.data.Get_Key = true;
        yield return null;
    }

    IEnumerator Get_Material()
    {
        GameObject go = FindAnyObjectByType<Chest>().gameObject;
        go.GetComponent<Animator>().Play("Open");
        instance.data.Get_Material = true;
        yield return null;
    }

    IEnumerator TalkJam()
    {
        instance.data.Talk_Jam = true;
        yield return null;
    }

    IEnumerator Clear_Jam()
    {
        instance.game.canTalk = true;
        Destroy(InteractObj);
        instance.data.Clear_Jam = true;
        GameObject go = FindAnyObjectByType<Jam>().gameObject;
        go.SetActive(false);
        go = FindAnyObjectByType<Bujeok>().gameObject;
        go.gameObject.layer = 7;
        Instantiate(Resources.Load<GameObject>("Cut/Clear_Jam"));
        yield return null;
    }

    IEnumerator Get_Bujeok()
    {
        instance.data.Get_Bujeok = true;
        Destroy(InteractObj);
        instance.game.canTalk = true;
        GameObject go = FindAnyObjectByType<Bujeok>().gameObject;
        Destroy(go);
        yield return null;
    }

    IEnumerator Use_Bujeok()
    {
        instance.data.Use_Bujeok = true;
        GameObject go = FindAnyObjectByType<Pillar>().gameObject;
        go.GetComponent<Animator>().Play("Rune2");
        yield return null;
    }

    IEnumerator Choice2()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("UI/UI_Choice2"));
        go.GetComponent<UI_Choice2>().talk = InteractObj;
        yield return null;
    }

    IEnumerator HappyEnd()
    {
        Destroy(InteractObj);
        Instantiate(Resources.Load<GameObject>("Cut/HappyEnd"));
        yield return null;
    }
    #endregion
}
