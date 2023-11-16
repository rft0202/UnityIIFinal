using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform[] waypoints;

    bool arrived=false, patrolling;
    int destination;

    public bool alerted;

    public Transform eye, target;

    Vector3 lastPos;

    public float patrolWaitTime, viewDist, viewAngle;

    public LayerMask playerMask;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolling = true;
        lastPos = transform.position;
        target = GameObject.Find("Player").transform;
    }

    bool CanSeePlayer()
    {
        if (Vector3.Distance(eye.position, target.position) < viewDist)
        {
            Vector3 playerDir = (target.position - eye.position).normalized;
            float angDiff = Vector3.Angle(eye.forward,playerDir);
            if (angDiff < viewAngle/2)
            {
                if (!Physics.Linecast(eye.position, target.position, ~playerMask))
                {
                    //enemy sees player
                    lastPos = target.position;
                    return true;
                }
            }
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.pathPending) { return; }

        //Code for patrolling
        if (patrolling)
        {
            //anim.SetBool("Attack", false);
            if (agent.remainingDistance < agent.stoppingDistance) //Arrived
            {
                if (!arrived)
                {
                    arrived = true;
                    StartCoroutine(GoToNextWaypoint());
                }
            }
        }
        //Code for enemy sees player
        if (CanSeePlayer()||alerted)
        {
            agent.SetDestination(target.position);
            patrolling = false;
            //setup attack
            //anim.SetBool("Attack", (agent.remainingDistance < agent.stoppingDistance));
        }
        else
        {
            if (!patrolling)
            {
                agent.SetDestination(lastPos);
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    patrolling = true;
                    StartCoroutine(GoToNextWaypoint());
                }
            }
        }
        //Play Move Animation
        //anim.SetFloat("Moving", agent.velocity.sqrMagnitude);
    }

    IEnumerator GoToNextWaypoint()
    {
        Vector3 dest = lastPos;
        if (waypoints.Length == 0) //Break out of coroutine if no waypoints
        {
            dest = new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y, transform.position.z + Random.Range(-5, 5));
        }
        else
        {
            dest = waypoints[destination].position;
        }

        patrolling = true;
        yield return new WaitForSeconds(patrolWaitTime);
        arrived = false;
        agent.destination = dest;
        destination = (destination == waypoints.Length-1) ? (0) : (destination + 1);

    }

    public IEnumerator Unalert()
    {
        yield return new WaitForSeconds(3f);
        alerted = false;
    }
}
