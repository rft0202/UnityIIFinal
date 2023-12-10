using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatAnim : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;

    public bool isSleeping,isSitting;

    public bool isCollectable = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if(isSleeping)
        {
            anim.SetBool("sleeping", true);
        }
        else if(isSitting)
        {
            anim.SetBool("sitting", true);
        }

        if(isCollectable)
        {
            DontDestroyOnLoad(gameObject);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("movement", agent.velocity.magnitude);
    }
}
