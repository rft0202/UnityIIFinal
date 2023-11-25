using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyJump : MonoBehaviour
{
    public float jumpHeight=2f, jumpDuration=0.6f;
    public Animator anim;

    IEnumerator Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;
        while (true)
        {
            if (agent.isOnOffMeshLink)
            {
                //anim.SetTrigger("Jump");
                yield return StartCoroutine(Jump(agent,jumpHeight,jumpDuration));
                agent.CompleteOffMeshLink();
            }
            yield return null;
        }
    }

    IEnumerator Jump(NavMeshAgent agent, float height, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endpos = data.endPos;
        Debug.Log(endpos);
        if (endpos == Vector3.zero) { //Uh oh something wrong!
            Vector3 player = GameObject.Find("Player").transform.position;
            endpos = new Vector3(player.x,player.y-1,player.z);
        }

        float time = 0.0f;

        while (time < 1.0f)
        {
            float upDist = height*(time-time*time);
            agent.transform.position = Vector3.Lerp(startPos,endpos,time)+(Vector3.up*upDist);

            time += Time.deltaTime/duration;
            yield return null;
        }
    }
}
