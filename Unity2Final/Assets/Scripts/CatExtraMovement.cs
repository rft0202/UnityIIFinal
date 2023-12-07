using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatExtraMovement : MonoBehaviour
{
    public Transform[] points;
    public bool[] pointSkip;
    NavMeshAgent agent;
    int currPoint=0;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currPoint<points.Length && pointSkip[currPoint])
        {
            NextPoint();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NextPoint(other);
        }
    }

    void NextPoint(Collider other=null)
    {
        if (agent.remainingDistance < agent.stoppingDistance && agent.pathStatus != NavMeshPathStatus.PathPartial)
        {
            if (currPoint < points.Length)
            {
                agent.SetDestination(points[currPoint].position);
                currPoint++;
            }
            else
            {
                other.gameObject.GetComponent<CatCollector>().CollectCat(gameObject);
                GetComponent<CatFollow>().enabled = true;
                GetComponent<CatExtraMovement>().enabled = false;
            }
        }
    }
}
