using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatAnim : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;

    public bool isSleeping;
    public bool isSitting;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if(isSleeping)
        {
            anim.SetBool("sleeping", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("movement", agent.velocity.magnitude);
    }
}
