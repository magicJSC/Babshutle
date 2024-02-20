using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GUController : MonoBehaviour
{
    [SerializeField] GameObject target;
    NavMeshAgent agent;
    Animator anim;
    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        anim = GetComponent<Animator>();
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
    }
}
