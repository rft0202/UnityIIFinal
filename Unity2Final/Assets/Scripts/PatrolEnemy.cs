using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform[] waypoints;

    bool arrived=false, patrolling, invincible=false, attacking=false;
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
        anim = GetComponent<Animator>();
        //target = GameObject.Find("Player").transform;
        StartCoroutine(Attack());
    }

    private void Awake()
    {
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
            if (agent.remainingDistance < agent.stoppingDistance && !attacking)
            {
                attacking = true;
                StartCoroutine(Attack());
            }
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
        anim.SetFloat("Moving", agent.velocity.sqrMagnitude);
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

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitObj = collision.gameObject;
        if (hitObj.GetComponent<PickupObj>() != null) //Hit by pickup obj
        {
            if (!invincible && hitObj.GetComponent<PickupObj>().canDamage)
            {
                GetComponent<EnemyHealth>().TakeDamage(50);
                if(GetComponent<EnemyHealth>().currHp>0) StartCoroutine(damageCooldown());
            }
        }
    }

    public IEnumerator Attack()
    {
        GetComponent<EnemyAttack>().Attack();
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(1.5f);
        attacking = false;
    }

    IEnumerator damageCooldown()
    {
        invincible = true;
        yield return new WaitForSeconds(0.5f);
        invincible = false;
    }
}
