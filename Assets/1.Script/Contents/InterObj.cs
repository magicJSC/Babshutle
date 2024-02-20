using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterObj : MonoBehaviour
{
    [Tooltip("대화가 여러번 할 필요없는 일회용 오브젝트")]
    [Serializable]
    public class Talk
    {
        public string speak;
        public string name;
        public Sprite character;
        public string eventName;
        public GameObject eventObj;
    }

    [SerializeField] public Talk[] talks;

    public void StartTalk()
    {
        GameObject t = Instantiate(Resources.Load<GameObject>("UI/UI_TextBox"));
        t.GetComponent<UI_TextBox>().inter = gameObject.GetComponent<InterObj>();
    }
}
