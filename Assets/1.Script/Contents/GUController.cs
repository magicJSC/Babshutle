using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class GUController : MonoBehaviour
{
    float curTime;

    public GameObject target;
    public NavMeshAgent agent;
    Animator anim;
    void Start()
    {
        Managers.Game.gu = gameObject;
        agent= GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        anim = GetComponent<Animator>();
        if(Managers.Data.Tutorial)
            gameObject.SetActive(false);
    }

    
    void Update()
    {
        agent.SetDestination(target.transform.position);

        if(Mathf.Abs(agent.velocity.x) > Mathf.Abs(agent.velocity.y))
        {
            if (agent.velocity.x > 0)
                anim.Play("RightMove");
            else if (agent.velocity.x < 0)
                anim.Play("LeftMove");
        }
        else if (Mathf.Abs(agent.velocity.x) < Mathf.Abs(agent.velocity.y))
        {
            if (agent.velocity.y > 0)
                anim.Play("UpMove");
            else if (agent.velocity.y < 0)
                anim.Play("DownMove");
        }

        SpeedUp();
    }

    void SpeedUp()
    {
        if(curTime >= 5)
        {
            curTime = 0;
            agent.speed += 0.2f;
        }
        else 
            curTime += Time.deltaTime;
    }
    void FootStep()
    {
        GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/FootStep"));
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<PlayerController>() != null)
        {
            Instantiate(Resources.Load<GameObject>("Cut/GameOver"));
            Managers.Game.canTalk = false;
            Managers.Game.GameOver = true;
        }
    }
}
