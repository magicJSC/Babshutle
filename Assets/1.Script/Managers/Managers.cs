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
        if (go != null)  //��ȣ�ۿ��� ������Ʈ�� �̿��� �̺�Ʈ�� �μ��� �־��ش�
        {
            InteractObj = go;
        }
        StartCoroutine(eventName);
    }

    #region �̺�Ʈ �Լ�
    IEnumerator Choice1()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("UI/UI_Choice1"));
        go.GetComponent<UI_Choice1>().talk = InteractObj;
        yield return null;
    }


    IEnumerator Explain()
    {
        Destroy(InteractObj);
        Instantiate(Resources.Load<GameObject>("Cut/Explain"));
        yield return null;
    }
    #endregion
}
