using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterObj : MonoBehaviour
{
    [Tooltip("��ȭ�� ������ �� �ʿ���� ��ȸ�� ������Ʈ")]
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
