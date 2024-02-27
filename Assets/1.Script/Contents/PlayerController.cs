using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int speed;
    Vector2 dir = Vector2.zero;
    Vector3 interDir = Vector3.down;
    Rigidbody2D rigid;
    Animator anim;
    void Start()
    {
        Managers.Game.player = gameObject;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Managers.Game.canTalk = true;
        Managers.Game.GameOver = false;
        if (!Managers.Data.Tutorial)
            transform.position = new Vector2(14.41f, -2.71f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Managers.Game.canTalk || Managers.Game.GameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
            rigid.velocity = Vector2.zero;
            anim.SetInteger("H", (int)rigid.velocity.x);
            anim.SetInteger("V", (int)rigid.velocity.y);
            return;
        }
        Move();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h > 0)
            anim.Play("RightMove");
        else if (h < 0)
            anim.Play("LeftMove");
        else if (v > 0)
            anim.Play("UpMove");
        else if (v < 0)
            anim.Play("DownMove");

        dir = new Vector2 (h, v);
        if(dir !=  Vector2.zero)
            interDir = dir;
        rigid.velocity = dir * speed;
        anim.SetInteger("H", (int)h);
        anim.SetInteger("V", (int)v);
    }

    void Interact()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position + interDir,new Vector2(1f,2f),0);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.layer == 7)
            {
                if (Managers.Data.Tutorial && col.GetComponent<Pikachu>() == null)
                    break;

                CheckObj(col.gameObject);
                break;
            }
        }
    }

    void CheckObj(GameObject go)
    {
        if (go.GetComponent<InterObj>() != null)
        {
            go.GetComponent<InterObj>().StartTalk();
        }
        else
        {
            go.GetComponent<Npc>().CheckObj();
        }
    }

    void FootStep()
    {
        GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/FootStep"));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + interDir, new Vector2(1f, 2f));
    }


}
