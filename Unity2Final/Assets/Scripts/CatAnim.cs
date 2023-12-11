using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CatAnim : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;

    public bool isSleeping,isSitting;

    public bool isCollectable = true;

    [NonSerialized]
    public string fromScene;

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
            fromScene = SceneManager.GetActiveScene().name;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("movement", agent.velocity.magnitude);
    }
}
