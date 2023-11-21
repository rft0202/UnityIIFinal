using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CatFollow : MonoBehaviour
{
    //public float spd;
    Transform player;
    NavMeshAgent agent;
    GameObject plat;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 playerPos = new Vector3(player.position.x, player.position.y - 0.8f, player.position.z);
        transform.LookAt(playerPos);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, (Vector3.Distance(transform.position,playerPos))*spd*Time.deltaTime);*/
        agent.SetDestination(player.position);
        if (plat != null)
        {
            transform.position = new Vector3(transform.position.x, plat.transform.position.y, transform.position.z);
        }
    }

    public void onPlatform(GameObject _p)
    {
        plat = _p;
    }

    public void offPlatform()
    {
        plat = null;
    }
}
